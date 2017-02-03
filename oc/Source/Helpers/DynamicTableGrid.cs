using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace oc.Source.Helpers
{
    public class CustomGrid<T> where T : View
    {
       
        private Grid NewGrid { get; set; }

        private IList<IList<T>> GridElements { get; set; }

        public CustomGrid(IList<IList<T>> gridElements, int spacing = 1)
        {
            InitNewGrid(gridElements, spacing);
            
        }

        public Grid GetGrid => NewGrid;

        private void InitNewGrid(IList<IList<T>> gridElements, int spacing)
        {
            GridElements = gridElements;
            var rowDefinitions = new RowDefinitionCollection();
            var columnDefinitions = new ColumnDefinitionCollection();

            foreach (var column in gridElements)
            {
                if (column.Count > 0)
                {
                    var columnDefinitionWidth = column.Last().WidthRequest;
                    columnDefinitions.Add(new ColumnDefinition
                    {
                        Width = columnDefinitionWidth > 0 ? columnDefinitionWidth : new GridLength(1, GridUnitType.Star),

                    });
                }
                
            }

            foreach (var row in gridElements.First())
            {
               rowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            NewGrid = new Grid
            {
                RowDefinitions = rowDefinitions,
                ColumnDefinitions = columnDefinitions,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#cccccc"),
                ColumnSpacing = spacing,
                RowSpacing = spacing

            };

            for (var index = 0; index < gridElements.Count; index++)
            {
                for (var i = 0; i < gridElements[index].Count; i++)
                {
                    var element = gridElements[index][i];
                    NewGrid.Children.Add(element, index, i);
                }

            }
        }
    }
}
