using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Utilities.Reactive
{
    public class ReactiveProperty<T> : ReactivePropertyBase<T>, IReactiveProperty, IObserver<T>
    {
        /// <summary>
        /// Get latestValue or push(set) value.
        /// </summary>
        public T Value
        {
            get { return InternalValue; }
            set { InternalValue = value; } 
        }

        object IReactiveProperty.Value
        {
            get { return Value; }
        }

        /// <summary>PropertyChanged raise on UIDispatcherScheduler</summary>
        public ReactiveProperty()
            : this(
                default(T), ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe
                )
        {
        }

        /// <summary>PropertyChanged raise on UIDispatcherScheduler</summary>
        public ReactiveProperty(T initialValue = default(T),
            ReactivePropertyMode mode =
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe)
            : this(UIDispatcherScheduler.Default, initialValue, mode)
        {
        }

        /// <summary>PropertyChanged raise on selected scheduler</summary>
        public ReactiveProperty(IScheduler raiseEventScheduler,
            T initialValue = default(T),
            ReactivePropertyMode mode =
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe)
            : this(Observable.Never<T>(), raiseEventScheduler, initialValue, mode)
        {
        }

        // ToReactiveProperty Only
        internal ReactiveProperty(IObservable<T> source,
            T initialValue = default(T),
            ReactivePropertyMode mode =
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe)
            : this(source, UIDispatcherScheduler.Default, initialValue, mode)
        {
        }

        internal ReactiveProperty(IObservable<T> source,
            IScheduler raiseEventScheduler,
            T initialValue = default(T),
            ReactivePropertyMode mode =
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe) :
                    base(source, raiseEventScheduler, initialValue, mode)
        {
        }

        void IObserver<T>.OnCompleted()
        {
            
        }

        void IObserver<T>.OnError(Exception error)
        {
            
        }

        void IObserver<T>.OnNext(T value)
        {
            Value = value;
        }
    }
}