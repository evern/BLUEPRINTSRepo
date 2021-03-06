﻿using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.Data;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.ViewModel.UndoRedo
{
    public class EntitiesUndoRedoManager<TEntity>
        where TEntity : class
    {
        public Action<UndoRedoEntityInfo<TEntity>> FuncUndo { get; set; }
        public Action<UndoRedoEntityInfo<TEntity>> FuncRedo { get; set; }

        public EntitiesUndoRedoManager(Action<UndoRedoEntityInfo<TEntity>> funcUndo, Action<UndoRedoEntityInfo<TEntity>> funcRedo)
        {
            FuncUndo = funcUndo;
            FuncRedo = funcRedo;
        }

        #region Public Methods
        public void Clear()
        {
            UndoList.Clear();
            RedoList.Clear();
        }

        /// <summary>
        /// Specify whether any undo operation can be done
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            return UndoList.Count > 0;
        }

        /// <summary>
        /// Specify whether any redo operation can be done
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            return RedoList.Count > 0;
        }

        /// <summary>
        /// Adds a property from the view to the undo list
        /// </summary>
        /// <param name="entityHashCode">Hash code of the property instance</param>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="oldValue">Old value of the property</param>
        /// <param name="newValue">New value of the property</param>
        /// <param name="actionId">Undo/Redo action id</param>
        /// <param name="messageType">Action to take when undoing/redoing</param>
        public void AddUndo(TEntity changedEntity, string propertyName, object oldValue, object newValue, EntityMessageType messageType)
        {
            UndoList.Add(new UndoRedoEntityInfo<TEntity>(changedEntity, propertyName, oldValue, newValue, ActionId, messageType));
            RedoList.Clear();
        }

        /// <summary>
        /// Undo the last entity change
        /// </summary>
        public void Undo()
        {
            _IsInUndoRedoOperation = true;

            if (UndoList.Count > 0)
            {
                // Extract the item from the undo list.
                int undoActionId = UndoList.Last().ActionId;
                UndoRedoEntityInfo<TEntity> item;

                for (int i = UndoList.Count - 1; i >= 0; i--)
                {
                    item = UndoList.Last();
                    if (item.ActionId == undoActionId)
                    {
                        UndoList.RemoveAt(UndoList.Count - 1);
                        List<UndoRedoEntityInfo<TEntity>> copyRedoList = RedoList.ToList();
                        copyRedoList.Add(item);
                        // We need to copy the undo list here.
                        List<UndoRedoEntityInfo<TEntity>> copyUndoList = UndoList.ToList();
                        FuncUndo(item);
                        // Now repopulate the undo and redo lists.
                        UpdateRedoList(copyRedoList);
                        UndoList.Clear();
                        UndoList.AddRange(copyUndoList);
                    }
                }

                SetActionId(_ActionId - 1);
            }

            _IsInUndoRedoOperation = false;
        }

        /// <summary>
        /// Redo the last undone entity change
        /// </summary>
        /// <remarks>
        /// Unlike the undo operation, we don't need to copy the undo list out
        /// because we want the item we're redoing being added back to the redo
        /// list.
        /// </remarks>
        public void Redo()
        {
            _IsInUndoRedoOperation = true;

            if (RedoList.Count > 0)
            {
                // Extract the item from the redo list.
                int redoActionId = RedoList.Last().ActionId;
                UndoRedoEntityInfo<TEntity> item;

                for (int i = RedoList.Count - 1; i >= 0; i--)
                {
                    item = RedoList.Last();
                    if (item.ActionId == redoActionId)
                    {
                        // Now, remove it from the list.
                        RedoList.RemoveAt(RedoList.Count - 1);
                        // Here we need to copy the redo list out because
                        // we will clear the list when the Add is called and
                        // the Redo is cleared there.
                        List<UndoRedoEntityInfo<TEntity>> redoList = RedoList.ToList();
                        //Redo actionId should be the same as undo action id
                        SetActionId(item.ActionId);
                        // Redo the last operation.
                        FuncRedo(item);
                        // Add the last redo item into undo list
                        AddUndo(item.ChangedEntity, item.PropertyName, item.OldValue, item.NewValue, item.MessageType);
                        // Now reset the redo list.
                        UpdateRedoList(redoList);
                    }
                }
            }

            _IsInUndoRedoOperation = false;
        } 
        #endregion

        #region Private methods
        /// <summary>
        /// Refreshes the redo list with new entries
        /// </summary>
        /// <param name="redoList"></param>
        private void UpdateRedoList(List<UndoRedoEntityInfo<TEntity>> redoList)
        {
            RedoList.Clear();
            RedoList.AddRange(redoList);
        }

        List<UndoRedoEntityInfo<TEntity>> _undoList;
        /// <summary>
        /// Get the undo list.
        /// </summary>
        List<UndoRedoEntityInfo<TEntity>> UndoList
        {
            get
            {
                if (_undoList == null)
                    _undoList = new List<UndoRedoEntityInfo<TEntity>>();
                return _undoList;
            }
            set
            {
                _undoList = value;
            }
        }

        List<UndoRedoEntityInfo<TEntity>> _redoList;
        /// <summary>
        /// Get the redo list.
        /// </summary>
        List<UndoRedoEntityInfo<TEntity>> RedoList
        {
            get
            {
                if (_redoList == null)
                    _redoList = new List<UndoRedoEntityInfo<TEntity>>();
                return _redoList;
            }
            set
            {
                _redoList = value;
            }
        } 
        #endregion

        #region Action Id
        //allows undo/redo operation to be tagged with incremental action id
        int _ActionId = 0;
        //allows action id to be paused from increment for bulk operation
        bool _PauseActionId;
        //remember not to increment actionId when undo manager is in undo/redo operation
        bool _IsInUndoRedoOperation;
        /// <summary>
        /// Allows action id to be incremented everytime is is retrieved
        /// </summary>
        private int ActionId
        {
            get
            {
                if (_IsInUndoRedoOperation)
                {
                    return _ActionId;
                }
                else
                {
                    if (!_PauseActionId)
                        _ActionId += 1;

                    return _ActionId;
                }
            }
        }

        /// <summary>
        /// Set action id manually by undo/redo operation 
        /// </summary>
        /// <param name="actionId">Action Id to set</param>
        private void SetActionId(int actionId)
        {
            _ActionId = actionId;
        }

        /// <summary>
        /// Pause action id to record multiple property changes in one action
        /// </summary>
        public void PauseActionId()
        {
            if (!_PauseActionId) //sometimes pause can be called multiple times
                _ActionId += 1;

            _PauseActionId = true;
        }

        public void RewindActionId(int rewindValue)
        {
            _ActionId -= rewindValue;
        }

        /// <summary>
        /// Unpause action id to allow increment per property changes
        /// </summary>
        public void UnpauseActionId()
        {
            _PauseActionId = false;
        }

        public bool IsInUndoRedoOperation()
        {
            return _IsInUndoRedoOperation;
        }
        #endregion
    }
}
