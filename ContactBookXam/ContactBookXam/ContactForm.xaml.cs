using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBookXam.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactBookXam
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactForm : ContentPage
	{
	    public EntryCell FirstEntry
	    {
	        get { return EntryFirst; }
	    }

	    public EntryCell LastEntry
	    {
	        get { return EntryLast; }
	    }
	    public Button SaveBtn
	    {
	        get { return BtnSave; }
	    }
	    public ContactForm(Contact contact)
		{
		    

		    BindingContext = contact;

		    InitializeComponent ();
		}

	    public ContactForm()
	    {
	        InitializeComponent ();
	    }

	   
	}
}