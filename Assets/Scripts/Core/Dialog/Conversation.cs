﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Represnts a sequence of Dialogs masked as one dialog. 
    /// </summary>
    public class Conversation : Dialog
    {
        /// <summary>
        /// List of Dialogs to be presented in order. 
        /// </summary>
        public Dialog[] Dialogs;

        public override DialogPauseType Property
        {
            get
            {
                return Dialogs[Index].Property;
            }
        }

        /// <summary>
        /// Current Dialog index
        /// </summary>
        private int Index;

        /// <summary>
        /// Invoked when conversation starts.  
        /// </summary>
        public UnityEvent OnStartConversation;

        /// <summary>
        /// Invoked when conversation ends .  
        /// </summary>
        public UnityEvent OnEndConversation;

        /// <summary>
        /// Wraps current dialog gettings it's DisplayDelay time 
        /// </summary>
        /// <returns></returns>
        public override float GetDisplayDelay()
        {
            return Dialogs[Index].GetDisplayDelay();
        }

        /// <summary>
        /// True unless the last Dialog doesn't HasNext
        /// </summary>
        /// <returns></returns>
        public override bool HasNext()
        {
            return Index < Dialogs.Length-1 || Dialogs[Index].HasNext();
        }

        /// <summary>
        /// Returns next charachter from current dialog. When a dialog ends, returns a null charachter and move dialog to next. 
        /// </summary>
        /// <returns></returns>
        public override char NextCharachter()
        {
            if (Dialogs[Index].HasNext() == false)
            {
                Dialogs[Index].WhenDialogEnd();
                Index++;

                Dialogs[Index].WhenDialogStart();
                return '\0'; 
            }

            return Dialogs[Index].NextCharachter();
        }

        /// <summary>
        /// Wraps current dialog 
        /// </summary>
        public override void ResetDialog()
        {
            foreach (Dialog d in Dialogs)
            {
                d.ResetDialog();
            }

            Index = 0;
        }

        /// <summary>
        /// Ends conversation and current (last) dialog. 
        /// </summary>
        public override void WhenDialogEnd()
        {
            base.WhenDialogEnd();
            Dialogs[Index].WhenDialogEnd();
            OnEndConversation?.Invoke();
        }

        /// <summary>
        /// Wraos pause dialog 
        /// </summary>
        public override void WhenDialogPause()
        {
            base.WhenDialogPause(); 
            Dialogs[Index].WhenDialogPause();
        }

        /// <summary>
        /// Wraps resume dialog 
        /// </summary>
        public override void WhenDialogResume()
        {
            base.WhenDialogResume(); 
            Dialogs[Index].WhenDialogResume();
        }

        /// <summary>
        /// Starts conversation and first dialog
        /// </summary>
        public override void WhenDialogStart()
        {
            base.WhenDialogStart(); 
            OnStartConversation?.Invoke();
            Index = 0;
            Dialogs[Index].WhenDialogStart();
        }
    }
}
