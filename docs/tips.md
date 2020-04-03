# Tips

This page will contain some extra information that might help you out.

## Creating a grid

The M contains an `IGrid<IPlayablePuzzleSquare>`.
In the VM, you might want to make a corresponding `IGrid<SquareViewModel>`,
which you can then pass along to the `PiCrossControl`.

The easiest way to create his new grid is

```csharp
var vmGrid = playablePuzzle.Grid.Map( square => new SquareViewModel(square) ).Copy();
```

The `Map` operation creates a new grid of the same dimensions,
but where every element is a "transformation" of the element
at the corresponding position in `vmGrid`. In the code above,
each square in `playablePuzzle.Grid` is wrapped inside a
`SquareViewModel`. All these wrapper objects are then
put in a new grid.

The `Copy()` is not strictly necessary, but depending
on how you write your code, it might prevent some nasty surprises from happening.

## Bind on Properties, Not Fields!

```csharp
class MyViewModel
{
    public Cell<int> MyData;
}
```

```xml
<SomeControl SomeProperty={Binding MyData}">
```

This will not work: bindings only look for *properties*, not *fields*.
Fix this by upgrading `MyData` to a property:

```csharp
class MyViewModel
{
    public Cell<int> MyData { get; }
}
```

## Controls Not Appearing?

If controls don't appear as expected, you might have gotten your bindings wrong.
Below are a few pointers.

### Output Pane

* Run your application in debug mode (`F5` or press the play button).
* Open the Output pane (Debug -> Windows -> Output).
* Check for data binding error messages.

An example of such an error message is

```text
System.Windows.Data Error: 40 : BindingExpression path error: 'Foo' property not found on 'object' ''PlayablePuzzleConstraints' (HashCode=26571712)'. BindingExpression:Path=Foo; DataItem='PlayablePuzzleConstraints' (HashCode=26571712); target element is 'ItemsControl' (Name=''); target property is 'ItemsSource' (type 'IEnumerable')
```

This was caused by

```xml
<ItemsControl ItemsSource="{Binding Foo}">
```

The binding asks to look for a property named `Foo` on the current `DataContext`.
From the error message, you can see the `DataContext` is set to a `PlayablePuzzleConstraints` object.
If you look through this class's code, you'll find that `PlayablePuzzleConstraints` has indeed no property `Foo`.

## Deliberately Using Wrong Bindings

To aid in debugging, you can bind on a nonexisting property on purpose.
This is a trick I use quite often when helping out students: it often gives me some inkling of what might go wrong.

```xml
<SomeControl SomeProperty="{Binding JFlkjdfl}">
```

This will lead to one of two outcomes:

* You get *no* error message in the output pane.
  This means that the `DataContext` is `null`.
  If there is no `DataContext`, bindings will not be looked at and will therefore not cause error message to appear.
  So, if you get no error message, you probably forgot to initialize the `DataContext`.
* You do get an error message in the output pane.
  This will tell you what the `DataContext` is.
  Binding mistakes are often due to the `DataContext` not being set to the correct object.

Make sure the property is always spelled correctly.
Copy pasting here is actually advised.
I've had many cases where students simply got the capitalization wrong.

## That Damn Little Rectangle At The Top

If, when you run your application, you get an annoying little rectangle at the top: that's normal.

It appears if you run your application in debug mode (F5 or using the play button).
You can make it disappear by running it without debugging (CTRL+F5).

If you click on the rectangle, a series of buttons appears.
I use these to determine the `DataContext`s of your controls.
It works as follows:

* Press on the second button (tooltip says Select Element).
* You can now select any control in your window.
* Press the first button (Go to Live Visual Tree).
* Visual Studio takes over your screen and shows you the Live Visual Tree, i.e., a hierarchical overview of the structure of your main window.
* In this Live Visual Tree, the control you selected in a previous step should be highlighted.
* Right click on it and select Show Properties.
* The Live Property Explorer appears.
  It tells you the values of all the properties of the active control.
* Generally, I want to find out what the `DataContext` is.
  You can fill in `Data` in the search box at the top.
* You can view the class of the control's `DataContext`, as well as view its internals.
