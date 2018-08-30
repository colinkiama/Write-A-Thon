using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Events;
using Write_A_Thon.Model;

namespace Write_A_Thon.Services
{
    public class InfoBarService
    {
        public event EventHandler TargetChanged;
        public event EventHandler TargetTrackedChanged;

        private bool _isTargetTracked;

        public bool IsTargetTracked
        {
            get { return _isTargetTracked; }
            set
            {
                _isTargetTracked = value;
                RaiseTargetTrackedChanged();
            }
        }

        private void RaiseTargetTrackedChanged()
        {
            throw new NotImplementedException();
        }

        private uint _target;

        public uint Target
        {
            get { return _target; }
            set
            {
                _target = value;
                RaiseTargetChanged();
            }
        }

        private void RaiseTargetChanged()
        {
            TargetChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
