using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Enums
{
    public static class Enumarations
    {
        public enum FileOwnerShip
        {
            Owner = 0,
            User = 1
        }

        public enum CacheKey
        {
            Iller = 0,
            Kategoriler = 1
        }

        public enum Priority
        {
            Low = 0,
            Normal = 1,
            High = 2
        }

        public enum TaskStates
        {
            Active = 0,
            InActive = 1
        }

        public enum TaskStatus
        {
            Open = 1,
            Canceled = 2,
            Closed = 3,
            Waiting = 4,
            Assigned=5
        }

        public enum AssignTaskStatus
        {
            Open = 1,
            Assigned=2,
            Closed = 3,
            Waiting = 4,
            OtherAssigned = 5
        }

        public enum EmailStatus
        {
            Sended = 1,
            Error = 2,
            Waiting = 3,
            Sending = 4
        }

        public enum EventViewerSources
        {
            EmailService
        }

        public enum EventViewerLogNames
        {
            EmailServiceLog = 1
        }
    }
}
