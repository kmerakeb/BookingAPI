using Booking.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Instrastructure
{
    public class BusinessFactory
    {

        AddressFactory addressFactory = new AddressFactory();
        ServiceFactory serviceFactory = new ServiceFactory();
        public BusinessFactory()
        {

        }

        public DAO.Business CreateBusiness(Business business)
        {
            return new DAO.Business()
            {
                Id = business.id,
                BusinessName = business.BusinessName,
                BusinessType = business.BusinessType,
                Description = business.Description,
                BusinessCategoryId = business.BusinessCategoryId,
                LastUpdate = business.LastUpdate,
                DateCreated = business.DateCreated,
                openingHour = business.openingHour,
                ClosingHour = business.ClosingHour,
                CustomerId = business.CustomerId,

                Addresses = business.Addresses.Select(b => addressFactory.CreateAddress(b)).ToList(),
                Services = business.Services.Select(b => serviceFactory.CreateService(b)).ToList()

            };
        }



        public Business CreateBusiness(DAO.Business business)
        {
            return new Business()
            {
                id = business.Id,
                BusinessName = business.BusinessName,
                BusinessType = business.BusinessType,
                Description = business.Description,
                BusinessCategoryId = business.BusinessCategoryId,
                LastUpdate = business.LastUpdate,
                DateCreated = business.DateCreated,
                openingHour = business.openingHour,
                ClosingHour = business.ClosingHour,
                CustomerId = business.CustomerId,
                Addresses = business.Addresses == null ? new List<Address>() : business.Addresses.Select(a => addressFactory.CreateAddress(a)).ToList(),
                Services = business.Services == null ? new List<Service>() : business.Services.Select(a => serviceFactory.CreateService(a)).ToList()


            };
        }

        // helper method for the object that contains only the fields that are entred by the user
        public object CreateDataShapedObject(Business business, List<string> lstOfFields) // simple pass throu method
        {

            return CreateDataShapedObject(CreateBusiness(business), lstOfFields);
        }


        public object CreateDataShapedObject(DAO.Business business, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return business;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = business.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(business, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }





    }
}

