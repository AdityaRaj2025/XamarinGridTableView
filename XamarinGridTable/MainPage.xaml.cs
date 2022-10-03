using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGridTable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            colScrollView.Scrolled += ColumnScrollView_Scrolled;
            dataScrollView.Scrolled += DataScrollView_Scrolled;
            rowScrollView.Scrolled += RowScrollView_Scrolled;
            string[] times = new string[] { "11:30", "12:00", "12:30", "13:00", "13:30", "14:00" };
            string[] roomtypes = new string[] { "type0", "type1", "type2", "type3", "type4", "type5", "type0", "type1", "type2", "type3", "type4", "type5", "type6", "type7", "type8", "type9", "type10" };

            for (int i = 0; i < times.Length; i++)
            {
                timelist.ColumnDefinitions.Add(new ColumnDefinition());
                Label time = new Label() { Text = times[i], WidthRequest = 80 };
                time.BackgroundColor = Color.YellowGreen;
                time.HorizontalTextAlignment = TextAlignment.Center;
                time.VerticalTextAlignment = TextAlignment.Center;
                timelist.Children.Add(time, i, 0);
                time.GestureRecognizers.Add((new TapGestureRecognizer
                {
                    Command = new Command((obj) =>
                    {
                        Label a = obj as Label;
                        a.Text = "tapped";
                    }),
                    CommandParameter = time,
                }));
            }
            status.WidthRequest = timelist.Width;
            for (int i = 0; i < roomtypes.Length; i++)
            {
                roomtype.RowDefinitions.Add(new RowDefinition() { Height = 50 });
                Label room = new Label() { Text = roomtypes[i], WidthRequest = 80 };
                room.VerticalTextAlignment = TextAlignment.Center;
                room.HorizontalTextAlignment = TextAlignment.Center;
                room.BackgroundColor = Color.Green;
                roomtype.Children.Add(room, 0, i);
                room.GestureRecognizers.Add((new TapGestureRecognizer
                {
                    Command = new Command((obj) =>
                    {
                        Label a = obj as Label;
                        a.Text = "tapped";
                    }),
                    CommandParameter = room,
                }));
            }
            List<string[]> roomstatu = new List<string[]>()
            {
                new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"}, new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"},new string[] {"1","2","3","4","5","6"}
            };
            int index = 0;
            bool flag = false;
            foreach (var item in roomstatu)
            {
                status.RowDefinitions.Add(new RowDefinition() { Height = 50 });
                for (int i = 0; i < item.Length; i++)
                {
                    if (index < 1) { status.ColumnDefinitions.Add(new ColumnDefinition() { Width = 80 }); }

                    Label statuslabel = new Label() { Text = item[i], WidthRequest = 80 };
                    statuslabel.HorizontalTextAlignment = TextAlignment.Center;
                    statuslabel.VerticalTextAlignment = TextAlignment.Center;
                    statuslabel.BackgroundColor = Color.WhiteSmoke;
                    statuslabel.TextColor = Color.DarkBlue;
                    status.Children.Add(statuslabel, i, index);
                    statuslabel.GestureRecognizers.Add((new TapGestureRecognizer
                    {
                        Command = new Command((obj) =>
                        {
                            Label temp = obj as Label;
                            Grid gridtemp = temp.Parent as Grid;
                            int position = gridtemp.Children.IndexOf(temp);
                            int columnindex = position % timelist.Children.Count;
                            int rowindex = (position / roomtype.Children.Count) + 1;
                            Label text = timelist.Children[2] as Label;
                            string timevalue = ((Label)timelist.Children[columnindex]).Text;
                            string roomtypealue = ((Label)roomtype.Children[rowindex]).Text;
                            temp.Text = status.Width.ToString();

                        }),
                        CommandParameter = statuslabel,
                    }));
                }
                index++;
            }
        }
        private async void DataScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            await rowScrollView.ScrollToAsync(0, e.ScrollY, false);
            await colScrollView.ScrollToAsync(e.ScrollX, 0, false);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            status.WidthRequest = timelist.Width;
        }
        private async void RowScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            await dataScrollView.ScrollToAsync(dataScrollView.ScrollX, e.ScrollY, false);
        }
        private async void ColumnScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            await dataScrollView.ScrollToAsync(e.ScrollX, dataScrollView.ScrollY, false);

        }
    }
}
