using System.Linq;
using System.Windows;
using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise
{
    public partial class ManageEmployeesWindow : Window
    {
        private AnnuaireContext _context;

        public ManageEmployeesWindow()
        {
            InitializeComponent();
            _context = new AnnuaireContext();
            LoadServices();
            LoadSites();
            LoadEmployees();
        }

        private void LoadServices()
        {
            ServiceComboBox.ItemsSource = _context.Services.ToList();
            ServiceComboBox.DisplayMemberPath = "Nom";
            ServiceComboBox.SelectedValuePath = "Id";
        }

        private void LoadSites()
        {
            SiteComboBox.ItemsSource = _context.Sites.ToList();
            SiteComboBox.DisplayMemberPath = "Ville";
            SiteComboBox.SelectedValuePath = "Id";
        }

        private void LoadEmployees()
        {
            EmployeesListView.ItemsSource = _context.Employes.Include(e => e.Service).Include(e => e.Site).ToList();
        }

        private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NomTextBox.Text) && !string.IsNullOrEmpty(PrenomTextBox.Text))
            {
                var employe = new Employe
                {
                    Nom = NomTextBox.Text,
                    Prenom = PrenomTextBox.Text,
                    TelephoneFixe = TelephoneFixeTextBox.Text,
                    TelephonePortable = TelephonePortableTextBox.Text,
                    Email = EmailTextBox.Text,
                    ServiceId = (int)ServiceComboBox.SelectedValue,
                    SiteId = (int)SiteComboBox.SelectedValue
                };
                _context.Employes.Add(employe);
                _context.SaveChanges();
                LoadEmployees();
                ClearForm();
            }
        }

        private void OnEditEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employe selectedEmploye)
            {
                selectedEmploye.Nom = NomTextBox.Text;
                selectedEmploye.Prenom = PrenomTextBox.Text;
                selectedEmploye.TelephoneFixe = TelephoneFixeTextBox.Text;
                selectedEmploye.TelephonePortable = TelephonePortableTextBox.Text;
                selectedEmploye.Email = EmailTextBox.Text;
                selectedEmploye.ServiceId = (int)ServiceComboBox.SelectedValue;
                selectedEmploye.SiteId = (int)SiteComboBox.SelectedValue;
                _context.SaveChanges();
                LoadEmployees();
                ClearForm();
            }
        }

        private void OnDeleteEmployeeButtonClick(object sender, RoutedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employe selectedEmploye)
            {
                _context.Employes.Remove(selectedEmploye);
                _context.SaveChanges();
                LoadEmployees();
                ClearForm();
            }
        }

        private void OnEmployeeSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employe selectedEmploye)
            {
                NomTextBox.Text = selectedEmploye.Nom;
                PrenomTextBox.Text = selectedEmploye.Prenom;
                TelephoneFixeTextBox.Text = selectedEmploye.TelephoneFixe;
                TelephonePortableTextBox.Text = selectedEmploye.TelephonePortable;
                EmailTextBox.Text = selectedEmploye.Email;
                ServiceComboBox.SelectedValue = selectedEmploye.ServiceId;
                SiteComboBox.SelectedValue = selectedEmploye.SiteId;
            }
        }

        private void ClearForm()
        {
            NomTextBox.Clear();
            PrenomTextBox.Clear();
            TelephoneFixeTextBox.Clear();
            TelephonePortableTextBox.Clear();
            EmailTextBox.Clear();
            ServiceComboBox.SelectedIndex = -1;
            SiteComboBox.SelectedIndex = -1;
        }
    }
}

