using Booking.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Helpers;
using Booking.Repository.Instrastructure;

namespace Booking.Repository.Instrastructure
{
    public class BusinessCategoryFactory
    {
        BusinessFactory businessFactory = new BusinessFactory();

        public BusinessCategoryFactory()
        {

        }

        public BusinessCategory CreateBusinessCategory(Booking.DAO.BusinessCategory businessCategory)
        {
            return new BusinessCategory()
            {
                Description = businessCategory.Description,
                CategoryName = businessCategory.CategoryName,
                Id = businessCategory.Id,
             
                Businesses = businessCategory.Businesses == null ? new List<Business>() : businessCategory.Businesses.Select(b => businessFactory.CreateBusiness(b)).ToList()
            };
        }


        public DAO.BusinessCategory CreateBusinessCategory(BusinessCategory businessCategory)
        {
            return new DAO.BusinessCategory()
            {
                Description = businessCategory.Description,
                CategoryName = businessCategory.CategoryName,
                Id = businessCategory.Id,
                Businesses = businessCategory.Businesses.Select(b => businessFactory.CreateBusiness(b)).ToList()
            };
        }



        public object CreateDataShapedObject(BusinessCategory businessCategory, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreateBusinessCategory(businessCategory), lstOfFields);
        }


        public object CreateDataShapedObject(DAO.BusinessCategory businessCategory, List<string> lstOfFields)
        {
            // work with a new instance, as we'll manipulate this list in this method
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            if (!lstOfFieldsToWorkWith.Any())
            {
                return businessCategory;
            }
            else
            {

                // does it include any business-related field?
                var lstOfBusinessFields = lstOfFieldsToWorkWith.Where(f => f.Contains("businesses")).ToList();

                // if one of those fields is "businesses", we need to ensure the FULL business is returned.  If
                // it's only subfields, only those subfields have to be returned.

                bool returnPartialBusiness = lstOfBusinessFields.Any() && !lstOfBusinessFields.Contains("businesses");

                // if we don't want to return the full business, we need to know which fields
                if (returnPartialBusiness)
                {
                    // remove all business-related fields from the list of fields,
                    // as we will use the CreateDateShapedObject function in BusinessFactory
                    // for that.

                    lstOfFieldsToWorkWith.RemoveRange(lstOfBusinessFields);
                    lstOfBusinessFields = lstOfBusinessFields.Select(f => f.Substring(f.IndexOf(".") + 1)).ToList();

                }
                else
                {
                    // we shouldn't return a partial business, but the consumer might still have
                    // asked for a subfield together with the main field, ie: business,business.id.  We 
                    // need to remove those subfields in that case.

                    lstOfBusinessFields.Remove("businesses");
                    lstOfFieldsToWorkWith.RemoveRange(lstOfBusinessFields);
                }

                // create a new ExpandoObject & dynamically create the properties for this object

                // if we have an expense

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFieldsToWorkWith)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = businessCategory.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(businessCategory, null);
             

                        // add the field to the ExpandoObject
                        ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
  
                }

                if (returnPartialBusiness)
                {
                    // add a list of businesses, and in that, add all those businesses
                    List<object> businesses = new List<object>();
                    foreach (var business in businessCategory.Businesses)
                    {
                        businesses.Add(businessFactory.CreateDataShapedObject(business, lstOfBusinessFields));
                    }

                    ((IDictionary<String, Object>)objectToReturn).Add("businesses", businesses);
                }


                return objectToReturn;
            }
        }

    }
}
