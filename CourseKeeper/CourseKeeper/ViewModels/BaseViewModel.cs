using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using CourseKeeper.Models;
using CourseKeeper.Services;
using System.Text;
using System.Security.Cryptography;
using Plugin.LocalNotifications;

namespace CourseKeeper.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public void SetNotify(bool enabled, string title, string body, string type, int id, DateTime notifyTime)
        {
            // This absurd thing should create a pretty close to unique id number for a given course, assessment, etc
            // by combining the type + id into a string and hashing it, then converting the has to a numeric.
            int NotifID = BitConverter.ToInt32(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(type + id.ToString())), 0);
            if (enabled)
            {

                CrossLocalNotifications.Current.Show(title, body, NotifID, notifyTime);
            }
            else
            {
                CrossLocalNotifications.Current.Cancel(NotifID);
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RaiseAllProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        #endregion
    }
}
