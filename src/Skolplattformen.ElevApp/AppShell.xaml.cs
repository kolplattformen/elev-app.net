using CommunityToolkit.Mvvm.ComponentModel;

namespace Skolplattformen.ElevApp;

public partial class AppShell : Shell 
{
    private bool _isTeachersTabVisible = true;


    public bool IsTeachersTabVisible
    {
        get => _isTeachersTabVisible;
        set
        {
            _isTeachersTabVisible = value;
            OnPropertyChanged();
        }
    }



    public AppShell()
	{
		InitializeComponent();

        BindingContext = this;
    }

  
}
