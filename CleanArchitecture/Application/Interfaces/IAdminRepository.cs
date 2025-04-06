using Bonded.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminRepository
    {
        List<int> GetAnalytics();
        List<CarousalImage> GetCarousalImages();
        void addCarousalImage(string fileName);
        void RemoveCarousals(List<CarousalImage> images);
    }
}
