using MedicoRegister.portable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicoRegister.portable.Data
{
    public sealed class MedicoUserProvider
    {
        private static List<MedicoUser> _items;
        private static MedicoUserProvider _provider = null;

        private MedicoUserProvider()
        {
            _items = new List<MedicoUser>();
        }

        public static MedicoUserProvider GetProvider()
        {
            if (_provider == null)
                _provider = new MedicoUserProvider();

            return _provider;
        }

        public List<MedicoUser> Items { get { return _items; } private set { } }

        public void AddMedicoUser(String code, String numb, int imgId)
        {
            _items.Add(new MedicoUser() { MedicoCode = code, PhoneNumber = numb, ImageResourceId = imgId });
        }
    }
}
