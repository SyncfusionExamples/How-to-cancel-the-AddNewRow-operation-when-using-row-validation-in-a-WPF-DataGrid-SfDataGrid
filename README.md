# How to cancel the AddNewRow operation when using row validation in a WPF DataGrid (SfDataGrid)?

In [WPF DataGrid](https://www.syncfusion.com/wpf-controls/datagrid) (SfDataGrid), the [RowValidating](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.SfDataGrid.html#Syncfusion_UI_Xaml_Grid_SfDataGrid_RowValidating) event can be used to cancel the [AddNewRow](https://help.syncfusion.com/wpf/datagrid/data-manipulation#add-new-rows) operation and end the edit by calling the [CancelAddNew](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.GridAddNewRowController.html#Syncfusion_UI_Xaml_Grid_GridAddNewRowController_CancelAddNew) and [EndEdit](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.GridCurrentCellManager.html#Syncfusion_UI_Xaml_Grid_GridCurrentCellManager_EndEdit_System_Boolean_) methods.

When you attempt to commit a new row, if any cell contains validation errors, the row is not added to the DataGrid. However, in this scenario, the validation error message is not displayed.

If error messages need to be shown to the user, the row should not be cancelled during validation. Instead, the **AddNewRow** operation should remain active until all cells contain valid data.

**C#**
```
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
```

![Cancel Addnewrow operation](Cancel%20Addnewrow%20operation.gif)

Take a moment to peruse the [WPF DataGrid - Data Validation](https://help.syncfusion.com/wpf/datagrid/data-validation) documentation, to learn more about data validation with example.