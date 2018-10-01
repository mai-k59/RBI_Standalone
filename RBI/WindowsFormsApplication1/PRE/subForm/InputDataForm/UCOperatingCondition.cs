﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RBI.Object.ObjectMSSQL;
using RBI.BUS.BUSMSSQL;
using RBI.Object;

namespace RBI.PRE.subForm.InputDataForm
{
    public partial class UCOperatingCondition : UserControl
    {
        public UCOperatingCondition()
        {
            InitializeComponent();
        }
        public UCOperatingCondition(int ID)
        {
            InitializeComponent();
            getData(ID);
        }
        
        RW_EXTCOR_TEMPERATURE objTemp = new RW_EXTCOR_TEMPERATURE();
        private void getData(int ID)
        {
            RW_STREAM_BUS SteamBus = new RW_STREAM_BUS();
            RW_EXTCOR_TEMPERATURE_BUS tempBus = new RW_EXTCOR_TEMPERATURE_BUS();
            RW_STREAM objSteam = SteamBus.getData(ID);
            RW_EXTCOR_TEMPERATURE extTemp = tempBus.getData(ID);

            txtFlowRate.Text = objSteam.FlowRate.ToString();
            txtMaxOperatingPressure.Text = objSteam.MaxOperatingPressure.ToString();
            txtMinOperatingPressure.Text = objSteam.MinOperatingPressure.ToString();
            txtMaximumOperatingTemp.Text = objSteam.MaxOperatingTemperature.ToString();
            txtMinimumOperatingTemp.Text = objSteam.MinOperatingTemperature.ToString();
            txtCriticalExposure.Text = objSteam.CriticalExposureTemperature.ToString();
            txtOperatingHydrogen.Text = objSteam.H2SPartialPressure.ToString();

            txtOp12.Text = extTemp.Minus12ToMinus8.ToString();
            txtOp8.Text = extTemp.Minus8ToPlus6.ToString();
            txtOp6.Text = extTemp.Plus6ToPlus32.ToString();
            txtOp32.Text = extTemp.Plus32ToPlus71.ToString();
            txtOp71.Text = extTemp.Plus71ToPlus107.ToString();
            txtOp107.Text = extTemp.Plus107ToPlus121.ToString();
            txtOp121.Text = extTemp.Plus121ToPlus135.ToString();
            txtOp135.Text = extTemp.Plus135ToPlus162.ToString();
            txtOp162.Text = extTemp.Plus162ToPlus176.ToString();
            txtOp176.Text = extTemp.MoreThanPlus176.ToString();
        }
        
        public RW_STREAM getDataforStream(int ID)
        {
            RW_STREAM str = new RW_STREAM();
            str.ID = ID;
            str.FlowRate = txtFlowRate.Text != "" ? float.Parse(txtFlowRate.Text) : 0;
            str.MaxOperatingPressure = txtMaxOperatingPressure.Text != "" ? float.Parse(txtMaxOperatingPressure.Text) : 0;
            str.MinOperatingPressure = txtMinOperatingPressure.Text != "" ? float.Parse(txtMinOperatingPressure.Text) : 0;
            str.MaxOperatingTemperature = txtMaximumOperatingTemp.Text != "" ? float.Parse(txtMaximumOperatingTemp.Text) : 0;
            str.MinOperatingTemperature = txtMinimumOperatingTemp.Text != "" ? float.Parse(txtMinimumOperatingTemp.Text) : 0;
            str.CriticalExposureTemperature = txtCriticalExposure.Text != "" ? float.Parse(txtCriticalExposure.Text) : 0;
            str.H2SPartialPressure = txtOperatingHydrogen.Text != "" ? float.Parse(txtOperatingHydrogen.Text) : 0;
            return str;
        }
        public RW_EXTCOR_TEMPERATURE getDataExtcorTemp(int ID)
        {
            RW_EXTCOR_TEMPERATURE ext = new RW_EXTCOR_TEMPERATURE();
            ext.ID = ID;
            ext.Minus12ToMinus8 = txtOp12.Text != "" ? float.Parse(txtOp12.Text) : 0;
            ext.Minus8ToPlus6 = txtOp8.Text != "" ? float.Parse(txtOp8.Text) : 0;
            ext.Plus6ToPlus32 = txtOp6.Text != "" ? float.Parse(txtOp6.Text) : 0;
            ext.Plus32ToPlus71 = txtOp32.Text != "" ? float.Parse(txtOp32.Text) : 0;
            ext.Plus71ToPlus107 = txtOp71.Text != "" ? float.Parse(txtOp71.Text) : 0;
            ext.Plus107ToPlus121 = txtOp107.Text != "" ? float.Parse(txtOp107.Text) : 0;
            ext.Plus121ToPlus135 = txtOp121.Text != "" ? float.Parse(txtOp121.Text) : 0;
            ext.Plus135ToPlus162 = txtOp135.Text != "" ? float.Parse(txtOp135.Text) : 0;
            ext.Plus162ToPlus176 = txtOp162.Text != "" ? float.Parse(txtOp162.Text) : 0;
            ext.MoreThanPlus176 = txtOp176.Text != "" ? float.Parse(txtOp176.Text) : 0;
            return ext;
        }
        public RW_INPUT_CA_LEVEL_1 getDataforCA()
        {
            RW_INPUT_CA_LEVEL_1 ca = new RW_INPUT_CA_LEVEL_1();
            ca.Stored_Pressure = txtMinOperatingPressure.Text != "" ? float.Parse(txtMinOperatingPressure.Text) * 6.895f : 0;
            ca.Stored_Temp = txtMinimumOperatingTemp.Text != "" ? float.Parse(txtMinimumOperatingTemp.Text) + 273 : 0;
            return ca;
        }

        #region KeyPress Event Handle
        private void keyPressEvent(TextBox textbox, KeyPressEventArgs ev, bool percent)
        {
            
            string a = textbox.Text;
            if (percent)
            {
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && (ev.KeyChar != '.'))
                {
                    ev.Handled = true;
                }
                if(a.Contains(".") && ev.KeyChar == '.')
                {
                    ev.Handled = true;
                }
            }
            else
            {
                if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && (ev.KeyChar != '.') && (ev.KeyChar != '-'))
                {
                    ev.Handled = true;
                }
                if ((a.StartsWith("-") && ev.KeyChar == '-') || (a.Contains(".") && ev.KeyChar == '.'))
                {
                    ev.Handled = true;
                }
            }
        }
        private void txtMaximumOperatingTemp_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtMaximumOperatingTemp, e, false);
        }

        private void txtMinimumOperatingTemp_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtMinimumOperatingTemp, e, false);
        }

        private void txtOperatingHydrogen_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOperatingHydrogen, e, false);
        }

        private void txtCriticalExposure_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtCriticalExposure, e, false);
        }

        private void txtMaxOperatingPressure_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtMaximumOperatingTemp, e, false);
        }
        private void txtMinOperatingPressure_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtMinimumOperatingTemp, e, false);
        }

        private void txtFlowRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtFlowRate, e, false);
        }

        private void txtOp12_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp12, e, true);
        }

        private void txtOp8_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp8, e, true);
        }

        private void txtOp6_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp6, e, true);
        }

        private void txtOp32_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp32, e, true);
        }
        private void txtOp71_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp71, e, true);
        }

        private void txtOp107_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp107, e, true);
        }

        private void txtOp121_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp121, e, true);
        }

        private void txtOp135_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp135, e, true);
        }

        private void txtOp162_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp162, e, true);
        }

        private void txtOp176_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEvent(txtOp176, e, true);
        }
        private void checkOver100(TextBox txt)
        {
            DataChange++;
            if(txt.Text != "")
            {
                try
                {
                    if (float.Parse(txt.Text) > 100)
                    {
                        MessageBox.Show("Invalid value", "Cortek RBI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt.Text = "100";
                    }
                }
                catch
                {
                    txt.Text = "100";
                }
            }
        }
        

        private void txtOp12_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp12);
        }

        private void txtOp8_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp8);
        }

        private void txtOp6_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp6);
        }

        private void txtOp32_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp32);
        }

        private void txtOp71_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp71);
        }

        private void txtOp107_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp107);
        }

        private void txtOp121_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp121);
        }

        private void txtOp135_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp135);
        }

        private void txtOp162_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp162);
        }

        private void txtOp176_TextChanged(object sender, EventArgs e)
        {
            checkOver100(txtOp176);
        }
        #endregion


        #region Xu ly su kien khi data thay doi
        private int datachange = 0;
        private int ctrlSpress = 0;
        public event DataUCChangedHanlder DataChanged;
        public event CtrlSHandler CtrlS_Press;
        public int DataChange
        {
            get { return datachange; }
            set
            {
                datachange = value;
                OnDataChanged(new DataUCChangedEventArgs(datachange));
            }
        }
        public int CtrlSPress
        {
            get { return ctrlSpress; }
            set
            {
                ctrlSpress = value;
                OnCtrlS_Press(new CtrlSPressEventArgs(ctrlSpress));
            }
        }
        protected virtual void OnDataChanged(DataUCChangedEventArgs e)
        {
            if (DataChanged != null)
                DataChanged(this, e);
        }
        protected virtual void OnCtrlS_Press(CtrlSPressEventArgs e)
        {
            if (CtrlS_Press != null)
                CtrlS_Press(this, e);
        }
        private void txtMaximumOperatingTemp_TextChanged(object sender, EventArgs e)
        {
            DataChange++;
        }
        private void txtMaximumOperatingTemp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                CtrlSPress++;
            }
        }
        #endregion

        
        
    }
}
