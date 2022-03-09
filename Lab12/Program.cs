using System;
using System.Collections.Generic;

namespace Lab12
{

public enum CollectionOperation
    {
    Add,
    Remove,
    ValueChanged
    }

public interface IObservableCollection
    {
    event EventHandler<CollectionCahngedEventArgs> CollectionCahnged;
    string Name { get; }
    void Add(object value);
    void Remove(object value);
    }

public interface IChangeNotifing
    {
    event EventHandler NameChanged;
    }

public class CollectionCahngedEventArgs : EventArgs
    {

    public CollectionCahngedEventArgs(CollectionOperation operation, object value)
        {
        this.Operation = operation;
        this.Value = value;
        }

    public CollectionOperation Operation { get; private set; }
    public object Value { get; private set; }

    }

    public class ObservableCollection : IObservableCollection
    {
        private string name;
        private List<object> list;
        public event EventHandler<CollectionCahngedEventArgs> CollectionCahnged;

        public string Name { get => name; }

        public ObservableCollection(string name)
        {
            this.name = name;
            this.list = new List<object>();
        }

        public void Add(object value)
        {
            list.Add(value);
            CollectionCahngedEventArgs eventArgs = new CollectionCahngedEventArgs(CollectionOperation.Add, value);
            if(value is IChangeNotifing)
            {
                ((NotifingObject)value).NameChanged += NotifingObjectChangeHandler;
                eventArgs = new CollectionCahngedEventArgs(CollectionOperation.ValueChanged, value);
            }
            CollectionCahnged?.Invoke(this, eventArgs);
        }

        public void Remove(object value)
        {
            list.Remove(value);
            CollectionCahngedEventArgs eventArgs = new CollectionCahngedEventArgs(CollectionOperation.Remove, value);
            if (value is IChangeNotifing)
            {
                ((NotifingObject)value).NameChanged -= NotifingObjectChangeHandler;
            }
            else
                CollectionCahnged?.Invoke(this, eventArgs);
        }

        static void NotifingObjectChangeHandler(object sender, EventArgs args)
        {
            string action;
            if (((CollectionCahngedEventArgs)args).Operation == CollectionOperation.ValueChanged)
                action = "changed";
            else
                action = "added";
            Console.Write($"value: {(NotifingObject)sender} was {action}\n");
        }
    }

    public class SimpleWatcher
    {
        public void Watch(IObservableCollection ICollection)
        {
            ICollection.CollectionCahnged += CollectionChangedEventHandler;
        }
        public void StopWatching(IObservableCollection ICollection)
        {
            ICollection.CollectionCahnged -= CollectionChangedEventHandler;
        }
        static void CollectionChangedEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("Collection changed");
        }
    }

    public class SmartWatcher
    {
        public void Watch(IObservableCollection ICollection)
        {
            ICollection.CollectionCahnged += CollectionChangedEventHandler;
        }
        public void StopWatching(IObservableCollection ICollection)
        {
            ICollection.CollectionCahnged -= CollectionChangedEventHandler;
        }
        static void CollectionChangedEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("Collection changed");
            string action;
            if(((CollectionCahngedEventArgs)args).Operation == CollectionOperation.ValueChanged)
            {
                Console.Write($"collection: {((IObservableCollection)sender).Name}");
                return;
            }

            if (((CollectionCahngedEventArgs)args).Operation == CollectionOperation.Add)
                action = "added";
            else
                action = "removed";
            Console.WriteLine($"collection: {((IObservableCollection)sender).Name} changed, value: {((CollectionCahngedEventArgs)args).Value} was {action}");
        }
    }

    public class NotifingObject : IChangeNotifing
    {
        private string name;
        public string Name 
        {
            get => name;
            set 
            {
                name = value;
                NameChanged?.Invoke(this, new CollectionCahngedEventArgs(CollectionOperation.ValueChanged, value));
            }
        }

        public event EventHandler NameChanged;

        public NotifingObject()
        {            

        }
        public override string ToString()
        {
            return Name;
        }

    }

public class Program
    {

    public static void Main()
        {
        // ETAP 1
        Console.WriteLine("\nETAP 1\n");

            var collection = new ObservableCollection("[collection 1]");
            var simpleWatcher = new SimpleWatcher();

            collection.Add("[First item]");

            simpleWatcher.Watch(collection);

            collection.Add("[Second item]");
            collection.Remove("[First item]");

            // ETAP 2
            Console.WriteLine("\nETAP 2\n");

            var smartWatcher = new SmartWatcher();
            smartWatcher.Watch(collection);
            collection.Add("[Third item]");
            Console.WriteLine("-------------------------------");

            simpleWatcher.StopWatching(collection);
            collection.Remove("[Third item]");

            // ETAP 3
            Console.WriteLine("\nETAP 3\n");

            var object1 = new NotifingObject();
            var object2 = new NotifingObject();
            object1.Name = "[o1]";
            object2.Name = "[o2]";

            collection.Add(object1);
            collection.Add(object2);

            Console.WriteLine("-------------------------------");

            object1.Name = "[new o1]";
            object2.Name = "[new o2]";

            Console.WriteLine("-------------------------------");

            collection.Remove(object2);

            Console.WriteLine("-------------------------------");

            object1.Name = "[even newer o1]";
            object2.Name = "[even newer o2]";

            Console.WriteLine();
        }

    }

}
