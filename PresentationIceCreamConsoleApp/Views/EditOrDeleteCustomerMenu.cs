﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationIceCreamConsoleApp.Views
{
    public class EditOrDeleteCustomerMenu
    {
        private readonly DeleteCustomerMenu _deleteCustomerMenu;
        private readonly EditCustomer _editCustomer;
        private readonly AllCustomersMenu _allCustomersMenu;

        public EditOrDeleteCustomerMenu(DeleteCustomerMenu deleteCustomerMenu, EditCustomer editCustomer, AllCustomersMenu allCustomersMenu)
        {
            _deleteCustomerMenu = deleteCustomerMenu;
            _editCustomer = editCustomer;
            _allCustomersMenu = allCustomersMenu;
        }

        public void ShowEditOrDeleteCustomerMenu()
        {
            bool end = true;
            do
            {
                Console.Clear();
                Console.WriteLine("<Edit, Delete or Show Accounts>");
                Console.WriteLine("#########################");
                Console.WriteLine("1. Edit an Account");
                Console.WriteLine("2. Delete an Accuont");
                Console.WriteLine("3. Show all accuonts (Password = 'Bytmig123!')");
                Console.WriteLine("4. Go back to Main Menu");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _editCustomer.ShowEditCustomer();
                        break;
                    case "2":
                        _deleteCustomerMenu.ShowDeleteCustomer();
                        break;
                    case "3":
                        _allCustomersMenu.ShowAllCustomersMenu();
                        break;
                    case "4":
                        end = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        Thread.Sleep(1000);
                        break;
                }
            } while (end);

        }

    }
}
