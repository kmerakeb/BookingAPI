using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    public class EmployeeFactory
    {
        //AddressFactory addressFactory = new AddressFactory();
        //BookFactory bookFactory = new BookFactory();
        //ServiceFactory serviceFactory = new ServiceFactory();
        public EmployeeFactory()
        {

        }
        public Employee CreateEmployee(DAO.Employee employee)
        {


            return new Employee()
            {
                Id = employee.Id,
                EmpFirstName = employee.EmpFirstName,
                EmpLastName = employee.EmpLastName,
                EmpZip = employee.EmpZip,
                EmpRate = employee.EmpRate,
                EmpPhone = employee.EmpPhone,
                BusinessId = employee.BusinessId

                //Addresses = employee.Addresses == null ? new List<Address>() : employee.Addresses.Select(a => addressFactory.CreateAddress(a)).ToList(),

                //Books = employee.Books == null ? new List<Book>() : employee.Books.Select(a => bookFactory.CreateBook(a)).ToList(),
                //Services = employee.Services == null ? new List<Service>() : employee.Services.Select(a => serviceFactory.CreateService(a)).ToList()


            };
        }
        public DAO.Employee CreateEmployee(Employee employee)
        {
            return new DAO.Employee()
            {
                Id = employee.Id,
                EmpFirstName = employee.EmpFirstName,
                EmpLastName = employee.EmpLastName,
                EmpZip = employee.EmpZip,
                EmpRate = employee.EmpRate,
                EmpPhone = employee.EmpPhone,
                BusinessId = employee.BusinessId

                //Addresses = employee.Addresses.Select(s => addressFactory.CreateAddress(s)).ToList(),
                //Books = employee.Books.Select(s => bookFactory.CreateBook(s)).ToList(),
                //Services = employee.Services.Select(s => serviceFactory.CreateService(s)).ToList()

            };
        }

    }
}
