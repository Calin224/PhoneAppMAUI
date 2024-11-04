using PhoneApp.Model;
using PhoneApp.Services;

namespace PhoneApp;

public partial class MainPage : ContentPage
{
    private readonly DatabaseService _service;
    private int _editCustomerId;
    
    public MainPage(DatabaseService db)
    {
        InitializeComponent();
        _service = db;
        Task.Run(async ()=> ContactsListView.ItemsSource = await _service.GetContacts());
    }

    private async void saveButton_clicked(object? sender, EventArgs e)
    {
        if (_editCustomerId == 0)
        {
            var contact = new PhoneItem()
            {
                Name = NumeEntry.Text,
                PhoneNumber = NumarEntry.Text
            };
            await _service.Create(contact);
        }
        else
        {
            await _service.Update(new PhoneItem()
            {
                Id = _editCustomerId,
                Name = NumeEntry.Text,
                PhoneNumber = NumarEntry.Text,
            });

            _editCustomerId = 0;
        }

        NumeEntry.Text = string.Empty;
        NumarEntry.Text = string.Empty;
        
        ContactsListView.ItemsSource = await _service.GetContacts();
    }

    private async void listView_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        var contact = (PhoneItem)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

        switch (action)
        {
            case "Edit":
                _editCustomerId = contact.Id;
                NumeEntry.Text = contact.Name;
                NumarEntry.Text = contact.PhoneNumber;
                break;
            case "Delete":
                await _service.Delete(contact);
                ContactsListView.ItemsSource = await _service.GetContacts();
                break;
        }
    }
}