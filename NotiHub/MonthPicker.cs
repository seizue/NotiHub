using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class MonthPicker : Form
    {
        public int SelectedMonth { get; private set; }
        public int SelectedYear { get; private set; }
        private int currentYear;

        public MonthPicker(int initialMonth, int initialYear)
        {
            InitializeComponent();
            SelectedMonth = initialMonth;
            SelectedYear = initialYear;
            currentYear = initialYear;
            
            lbMonth.Text = currentYear.ToString();
            
            // Wire up month panel click events
            SetupMonthPanelEvents();
        }

        private void SetupMonthPanelEvents()
        {
            // Add click events to all month panels and labels
            AddMonthClickEvent(pJan, label1, 1);
            AddMonthClickEvent(pFeb, label2, 2);
            AddMonthClickEvent(pMar, label3, 3);
            AddMonthClickEvent(pApr, label4, 4);
            AddMonthClickEvent(pMay, label5, 5);
            AddMonthClickEvent(pJun, label6, 6);
            AddMonthClickEvent(pJul, label7, 7);
            AddMonthClickEvent(pAug, label8, 8);
            AddMonthClickEvent(pSep, label9, 9);
            AddMonthClickEvent(pOct, label10, 10);
            AddMonthClickEvent(pNov, label11, 11);
            AddMonthClickEvent(pDec, label12, 12);
            
            HighlightSelectedMonth();
        }

        private void AddMonthClickEvent(Control panel, Label label, int month)
        {
            panel.Click += (s, e) => SelectMonth(month);
            label.Click += (s, e) => SelectMonth(month);
            
            // Add hover effect
            panel.MouseEnter += (s, e) =>
            {
                if (!(currentYear == SelectedYear && month == SelectedMonth))
                {
                    panel.BackColor = Color.FromArgb(50, 58, 61);
                }
            };
            panel.MouseLeave += (s, e) =>
            {
                if (!(currentYear == SelectedYear && month == SelectedMonth))
                {
                    panel.BackColor = Color.FromArgb(38, 44, 48);
                }
            };
        }

        private void SelectMonth(int month)
        {
            SelectedMonth = month;
            SelectedYear = currentYear;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void HighlightSelectedMonth()
        {
            // Reset all panels
            ResetAllPanels();
            
            // Highlight selected month if viewing the selected year
            if (currentYear == SelectedYear)
            {
                Control selectedPanel = GetPanelForMonth(SelectedMonth);
                if (selectedPanel != null)
                {
                    selectedPanel.BackColor = Color.FromArgb(41, 128, 185);
                }
            }
        }

        private void ResetAllPanels()
        {
            pJan.BackColor = Color.FromArgb(38, 44, 48);
            pFeb.BackColor = Color.FromArgb(38, 44, 48);
            pMar.BackColor = Color.FromArgb(38, 44, 48);
            pApr.BackColor = Color.FromArgb(38, 44, 48);
            pMay.BackColor = Color.FromArgb(38, 44, 48);
            pJun.BackColor = Color.FromArgb(38, 44, 48);
            pJul.BackColor = Color.FromArgb(38, 44, 48);
            pAug.BackColor = Color.FromArgb(38, 44, 48);
            pSep.BackColor = Color.FromArgb(38, 44, 48);
            pOct.BackColor = Color.FromArgb(38, 44, 48);
            pNov.BackColor = Color.FromArgb(38, 44, 48);
            pDec.BackColor = Color.FromArgb(38, 44, 48);
        }

        private Control GetPanelForMonth(int month)
        {
            switch (month)
            {
                case 1: return pJan;
                case 2: return pFeb;
                case 3: return pMar;
                case 4: return pApr;
                case 5: return pMay;
                case 6: return pJun;
                case 7: return pJul;
                case 8: return pAug;
                case 9: return pSep;
                case 10: return pOct;
                case 11: return pNov;
                case 12: return pDec;
                default: return null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            currentYear++;
            lbMonth.Text = currentYear.ToString();
            HighlightSelectedMonth();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            currentYear--;
            lbMonth.Text = currentYear.ToString();
            HighlightSelectedMonth();
        }
    }
}
