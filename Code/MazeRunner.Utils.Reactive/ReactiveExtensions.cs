using System;

namespace MazeRunner.Utils.Reactive;

static public class ReactiveExtensions
{
    /// <summary>
    /// Subscribes to the source observable and diverts any exceptions thrown by the <paramref name="onNext"/> action to the <paramref name="onError"/> action.
    /// Note that in the case that the <paramref name="onError"/> action gets called due to an exception in the <paramref name="onNext"/> action the second parameter
    /// will be set to <c>false</c> to indicate that the exception did not come from the source observable.
    /// </summary>
    /// <remarks>Inspired by <a href="https://stackoverflow.com/a/79385334/863651">this SO post</a>.</remarks>
    static public IDisposable SubscribeAndHandleAllExceptions<T>(this IObservable<T> source, Action<T> onNext, Action<Exception, bool> onError)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(onNext);
        ArgumentNullException.ThrowIfNull(onError);


        return source.Subscribe(
            onNext: OnNextWithDivertedExceptions_,
            onError: onErrorFromSource_
        );

        void OnNextWithDivertedExceptions_(T obj_)
        {
            try
            {
                onNext(obj_);
            }
            catch (Exception ex)
            {
                onError(ex, /*comesFromSourceNotFromOnNext:*/ false);
            }
        }

        void onErrorFromSource_(Exception obj_)
        {
            onError(obj_, /*comesFromSourceNotFromOnNext:*/ true);
        }
    }
}
