using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using System.Diagnostics;
using AnnuaireEntreprise.Services.Site;

namespace AnnuaireEntreprise
{
    public partial class MainWindow : Window
    {
        private readonly AnnuaireContext _context;
        private readonly IServicesService _serviceService;
        private readonly ISiteService _sitesService;
        private List<Key> _konamiCode = new List<Key> { Key.Up, Key.Up, Key.Down, Key.Down, Key.Left, Key.Right, Key.Left, Key.Right };
        private Queue<Key> _inputKeys = new Queue<Key>();

        public MainWindow(IServicesService serviceService, ISiteService sitesService)
        {
            InitializeComponent();
            _context = new AnnuaireContext();
            _serviceService = serviceService;
            _sitesService = sitesService;
            LoadSites();
            LoadServices();
            LoadEmployees();
            this.KeyDown += OnKeyDown;
        }

        private void LoadSites()
        {
            SiteComboBox.ItemsSource = _sitesService.GetAllSites();
            SiteComboBox.DisplayMemberPath = "Ville";
            SiteComboBox.SelectedValuePath = "Id";
        }

        private void LoadServices()
        {
            ServiceComboBox.ItemsSource = _serviceService.GetAllServices();
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
                NomTextBlock.Text = selectedEmploye.Nom;
                PrenomTextBlock.Text = selectedEmploye.Prenom;
                TelephoneFixeTextBlock.Text = selectedEmploye.TelephoneFixe;
                TelephonePortableTextBlock.Text = selectedEmploye.TelephonePortable;
                EmailTextBlock.Text = selectedEmploye.Email;
                ServiceTextBlock.Text = selectedEmploye.Service?.Nom;
                SiteTextBlock.Text = selectedEmploye.Site?.Ville;
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _inputKeys.Enqueue(e.Key);
            Debug.WriteLine($"Key pressed: {e.Key}");

            if (_inputKeys.Count > _konamiCode.Count)
            {
                _inputKeys.Dequeue();
            }

            if (_inputKeys.SequenceEqual(_konamiCode))
            {
                Debug.WriteLine("Konami Code entered correctly!");
                ManagementButtonsPanel.Visibility = Visibility.Visible;
            }
            else if (_inputKeys.Count == _konamiCode.Count)
            {
                Debug.WriteLine("Incorrect Konami Code sequence.");
            }
        }
    }
}
