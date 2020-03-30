# `Chronometer` Class

The domain comes with a `Chronometer` class which you are free to use.
Below is explained what its purpose is.

## `DispatcherTimer`

Let's say you want to add a timer to your project.
This timer would show how many seconds have passed since starting the game.
This means that you need to perform the following actions each second:

* Increment the variable holding the number of seconds played.
* Update the visualization of the timer.

In order to implement this, you need a way to have these actions performed every second.
This is where `DispatcherTimer` comes in: it allows you to have a function called at regular intervals.

```c#
var timer = new DispatcherTimer();
timer.Interval = TimeSpan.FromSeconds(1);
timer.Tick += (s, e) => {
    // This code gets called every second
};
timer.Start();
```

You could now think that measuring how many seconds have passed is just a matter of writing

```c#
var totalSeconds = 0;
var timer = new DispatcherTimer();

timer.Interval = TimeSpan.FromSeconds(1);
timer.Tick += (s, e) => { totalSeconds++; };
timer.Start();
```

Unfortunately, this does not work as intended.

## The Problem With `DispatcherTimer`

When you're asking `DispatcherTimer` to call some function `Foo` every second, `Foo` will *not* be called exactly once every second.
Giving such an absolute guarantee would be too inefficient.
Instead, the only promise `DispatcherTimer` makes is that *at least* one second will pass between two `Foo` calls.

Here's some code that measures the time between two ticks:

```c#
public partial class MainWindow : Window
{
    private DispatcherTimer timer;

    public MainWindow()
    {
        InitializeComponent();
        var last = DateTime.Now;

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, o) =>
        {
            var now = DateTime.Now;
            var delta = now - last;
            last = now;
            Debug.WriteLine(delta.TotalMilliseconds);
        };
        timer.Start();
    }
}
```

Below are the values printed. If the `DispatcherTimer` were 100% accurate, we'd only see the value `1000` printed out.
However, our experiment shows this is far from being the case:

```text
1606.14
1218.6546
1158.091
1044.6101
1175.7924
1028.886
1016.274
1029.6044
1312.0138
1001.1929
1045.0662
1220.2431
1125.84
1074.9389
1298.4346
1061.2
1374.8002
1159.0104
1000.5774
1142.6014
```

The time measuring code shown above would be off by quite a bit:

* The first tick comes after 1.606 seconds, while the code counts this as merely 1 second.
* By the third tick, 3.982 seconds will actually have passed, whereas `totalSeconds` would be equal to `3`.
* By the 20th tick, 23 seconds will have passed.

As time passes, the error grows larger.

## Solution

Luckily, the solution to this problem is rather simple: instead of assuming that our function is called every second, we ask for the current time (`DateTime.Now`) and compare it with the last value.
This allows us to determine exactly how much time has passed since the last tick.

```c#
public partial class MainWindow : Window
{
    private DispatcherTimer timer;

    public MainWindow()
    {
        InitializeComponent();
        var totalTime = TimeSpan.Zero;
        var last = DateTime.Now;

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, o) =>
        {
            var now = DateTime.Now;
            var delta = now - last;
            last = now;
            totalTime += delta;
        };
        timer.Start();
    }
}
```

The above code measures time a lot more accurately: `totalTime` will be updated approximately once a second.
If you read this value just before a tick, `totalTime` will still contain an old value, so there still is an error.
However, this error will not grow boundlessly: at every tick, `totalTime` gets updated with a 100% accurate value, effectively resetting the error to zero.

## `Chronometer`

The `Chronometer` class can be used to accurately measure time.
It works as follows:

* It requires you to call `Tick()` at regular intervals. Any interval will do, but to keep the error down, you might want to call it every 250ms.
* The `Chronometer` can be paused and resumed using `Pause()` and `Start()`, respectively. You do not have to stop calling `Tick()`: the `Chronometer` will ignore ticks while it is paused.
* `TotalTime` contains the total time passed, taking into account pauses. The property has type `Cell<TimeSpan>`, meaning it is observable and you can bind a WPF control directly to it.

## Demo

A small WPF application making use of `Chronometer` can be found in the root of your repository.
