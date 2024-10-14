using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise
{
    public partial class MainWindow : Window
    {
        private AnnuaireContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new AnnuaireContext();
            LoadSites();
            LoadServices();
            LoadEmployees();
        }

        private void LoadSites()
        {
            SiteComboBox.ItemsSource = _context.Sites.ToList();
            SiteComboBox.DisplayMemberPath = "Ville";
            SiteComboBox.SelectedValuePath = "Id";
        }

        private void LoadServices()
        {
            ServiceComboBox.ItemsSource = _context.Services.ToList();
            ServiceComboBox.DisplayMemberPath = "Nom";
            ServiceComboBox.SelectedValuePath = "Id";
        }

        private void LoadEmployees()
        {
            EmployeesListView.ItemsSource = _context.Employes.Include(e => e.Service).Include(e => e.Site).ToList();
        }

        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            var query = _context.Employes.Include(e => e.Service).Include(e => e.Site).AsQueryable();

            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                query = query.Where(emp => emp.Nom.Contains(SearchTextBox.Text));
            }

            if (SiteComboBox.SelectedValue != null)
            {
                int siteId = (int)SiteComboBox.SelectedValue;
                query = query.Where(emp => emp.SiteId == siteId);
            }

            if (ServiceComboBox.SelectedValue != null)
            {
                int serviceId = (int)ServiceComboBox.SelectedValue;
                query = query.Where(emp => emp.ServiceId == serviceId);
            }

            EmployeesListView.ItemsSource = query.ToList();
        }

        private void EmployeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employe selectedEmploye)
            {
                ShowEmployeeDetails(selectedEmploye);
            }
        }

        private void OnManageSitesButtonClick(object sender, RoutedEventArgs e)
        {
            var manageSitesWindow = new ManageSitesWindow();
            manageSitesWindow.ShowDialog();
        }

        private void OnManageServicesButtonClick(object sender, RoutedEventArgs e)
        {
            var manageServicesWindow = new ManageServicesWindow();
            manageServicesWindow.ShowDialog();
        }

        private void OnManageEmployeesButtonClick(object sender, RoutedEventArgs e)
        {
            var manageEmployeesWindow = new ManageEmployeesWindow();
            manageEmployeesWindow.ShowDialog();
        }

        private void OnEmployeesListViewLeftClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employe selectedEmploye)
            {
                ShowEmployeeDetails(selectedEmploye);
            }
        }

        private void ShowEmployeeDetails(Employe employe)
        {
            NomTextBlock.Text = employe.Nom;
            PrenomTextBlock.Text = employe.Prenom;
            TelephoneFixeTextBlock.Text = employe.TelephoneFixe;
            TelephonePortableTextBlock.Text = employe.TelephonePortable;
            EmailTextBlock.Text = employe.Email;
            ServiceTextBlock.Text = employe.Service?.Nom;
            SiteTextBlock.Text = employe.Site?.Ville;
        }
    }
}
