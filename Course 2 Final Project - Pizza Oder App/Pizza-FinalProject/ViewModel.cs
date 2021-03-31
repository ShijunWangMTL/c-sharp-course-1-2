using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pizza_FinalProject.Domain.Order;
using static Pizza_FinalProject.Domain.Pizza;

namespace Pizza_FinalProject
{
    public class ViewModel
    {
        public ObservableCollection<PizzaSizeEnum> CmbSizeContent { get; private set; }
        public ObservableCollection<int> CmbQtyContent { get; private set; }
        public ObservableCollection<DeliverMethodEnum> CmbDeliveryContent { get; private set; }

        public ViewModel()
        {

            CmbSizeContent = new ObservableCollection<PizzaSizeEnum>((IEnumerable<PizzaSizeEnum>)Enum.GetValues(typeof(PizzaSizeEnum)));

            CmbQtyContent = new ObservableCollection<int>
             {
                 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
             };

            CmbDeliveryContent = new ObservableCollection<DeliverMethodEnum>((IEnumerable<DeliverMethodEnum>)Enum.GetValues(typeof(DeliverMethodEnum)));
        }
    }
}
