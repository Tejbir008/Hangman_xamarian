using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Hangman_xamarian.Resources;

namespace Hangman_xamarian
{
    [Activity(Label = "MainDB")]
    public class MainDB : Activity
    {

        List<Resources.HangmanScore> myList;
        private Button btnDeleteEntry;
        private Spinner spinnerEditDB;
        private Button btnEditDBMainMenu;
        //int Id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainDB);
            DBConnection myConnectionClass = new DBConnection();
            myList = myConnectionClass.ViewAll();

            btnEditDBMainMenu = FindViewById<Button>(Resource.Id.btnEditDBMainMenu);
            btnEditDBMainMenu.Click += btnEditDBMainMenu_Click;
            spinnerEditDB = FindViewById<Spinner>(Resource.Id.spinnereditDB);
            global::Hangman_xamarian.Resources.DataAdapter da = new Resources.DataAdapter(this, myList);

            spinnerEditDB.Adapter = da;

            spinnerEditDB.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerEditDB_ItemSelected);

            btnDeleteEntry = FindViewById<Button>(Resource.Id.btnDeleteEntry);
            btnDeleteEntry.Click += btnDeleteEntry_Click;
            btnDeleteEntry.Enabled = false;
        }
        private void btnEditDBMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            var cc = new DBConnection();
            cc.DeletePlayer(Hangman.Id);
            myList = cc.ViewAll();


            var da = new Resources.DataAdapter(this, myList);

            spinnerEditDB.Adapter = da;
        }

        private void spinnerEditDB_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {


            Spinner spinner = (Spinner)sender;
            Hangman.Id = this.myList.ElementAt(e.Position).Id;
            btnDeleteEntry.Enabled = true;

        }
    }
}