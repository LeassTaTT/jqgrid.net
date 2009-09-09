using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Permissions;
using System.Drawing.Design;
using System.Web.UI;

namespace Trirand.Web.UI.WebControls
{    
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class BaseItem : IStateManager, INamingContainer
    {
        private object _dataItem;
        private bool _isTrackingViewState;
        private StateBag _statebag = new StateBag();        

        
        public BaseItem()
        {
        }       

        internal void SetDirty()
        {
            ViewState.SetDirty(true);
        }


        protected virtual bool IsTrackingViewState
        {
            get
            {
                return _isTrackingViewState;
            }
        }

        protected virtual void LoadViewState(object savedState)
        {
            object[] state = (object[])savedState;

            if (state.Length != 1)
            {
                throw new ArgumentException("Invalid View State");
            }

            ((IStateManager)ViewState).LoadViewState(state[0]);
         
        }        

        protected virtual object SaveViewState()
        {
            object[] state = new object[1];

            if (ViewState != null)
            {
                state[0] = ((IStateManager)ViewState).SaveViewState();                
            }

            return state;
        }

        protected virtual void TrackViewState()
        {
            _isTrackingViewState = true;
            if (ViewState != null)
            {
                ((IStateManager)ViewState).TrackViewState();                
            }
        }       
        

        protected StateBag ViewState
        {
            get
            {
                return this._statebag;
            }
        }


        #region IStateManager Members

        bool IStateManager.IsTrackingViewState
        {
            get { return this.IsTrackingViewState; }
        }

        void IStateManager.LoadViewState(object state)
        {
            this.LoadViewState(state);
        }

        object IStateManager.SaveViewState()
        {
            return this.SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            this.TrackViewState();
        }

        #endregion
    }

}
