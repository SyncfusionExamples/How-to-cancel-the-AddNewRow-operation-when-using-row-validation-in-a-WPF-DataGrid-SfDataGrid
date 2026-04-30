using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataGridDemo
{
    public class OrderInfoCollection
    {
        private ObservableCollection<OrderInfo> _orders;

        public ObservableCollection<OrderInfo> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        private ICommand _rowValidatingCommand;
        public ICommand RowValidatingCommand
        {
            get
            {
                if (_rowValidatingCommand == null)
                    _rowValidatingCommand = new RelayCommand<RowValidatingEventArgs>(OnRowValidating);
                return _rowValidatingCommand;
            }
        }

        private void OnRowValidating(RowValidatingEventArgs args)
        {
            var dataGrid = args.OriginalSender as SfDataGrid;
            var data = args.RowData as OrderInfo;

            if (dataGrid == null || data == null)
                return;
            if (dataGrid.IsAddNewIndex(args.RowIndex))
            {
                if (data.OrderID < 1000)
                {
                    args.ErrorMessages.Add("OrderID", "OrderID should not below 1000.");
                    if (dataGrid.View.IsAddingNew)
                    {
                        if (dataGrid.SelectionController.CurrentCellManager.CurrentCell.IsEditing)
                            dataGrid.SelectionController.CurrentCellManager.EndEdit(true);
                        dataGrid.GetAddNewRowController().CancelAddNew();
                    }
                }
            } 
        }

        public OrderInfoCollection()
        {
            _orders = new ObservableCollection<OrderInfo>();
            this.GenerateOrders();
        }

        private void GenerateOrders()
        {
            _orders.Add(new OrderInfo(1001, "Maria", "Germany", "ALFKI", "Berlin"));
            _orders.Add(new OrderInfo(1002, "Trujilo", "Mexico", "ANATR", "Mexico D.F."));
            _orders.Add(new OrderInfo(1003, "Antonio", "Mexico", "ANTON", "Mexico D.F."));
            _orders.Add(new OrderInfo(1004, "Thomas", "UK", "AROUT", "London"));
            _orders.Add(new OrderInfo(1005, "Christina", "Sweden", "BERGS", "Lula"));
            _orders.Add(new OrderInfo(1006, "Hanna", "Germany", "BLAUS", "Mannheim"));
            _orders.Add(new OrderInfo(1007, "Citeaux", "France", "BLONP", "Strasbourg"));
            _orders.Add(new OrderInfo(1008, "Martin", "Spain", "BOLID", "Madrid"));
            _orders.Add(new OrderInfo(1009, "Laurence", "France", "BONAP", "Marseille"));
            _orders.Add(new OrderInfo(1010, "Elizabeth", "Canada", "BOTTM", "Tsawassen"));
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return _canExecute == null || _canExecute(default(T));
            }
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

