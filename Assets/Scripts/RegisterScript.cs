using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; 
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour
{
    // Variables GUI
    [SerializeField] private GameObject m_interfazloginUI = null;
    [SerializeField] private GameObject m_interfazregistrarUI = null;
    [SerializeField] private GameObject m_PanelAvisoUI = null;
    [SerializeField] private GameObject m_BackgroundUI = null;
    [SerializeField] private GameObject m_TextNomUI = null;
    [SerializeField] private GameObject m_TextApePatUI = null;
    [SerializeField] private GameObject m_TextApeMatUI = null;
    [SerializeField] private GameObject m_TextFecNacUI = null;
    [SerializeField] private GameObject m_TextPaisUI = null;
    [SerializeField] private GameObject m_TextCorreoUI = null;
    [SerializeField] private GameObject m_TextContraUI = null;
    [SerializeField] private GameObject m_TextConfContraUI = null;
    [SerializeField] private GameObject m_TextContrasNoCoincidenUI = null;
    [SerializeField] private GameObject m_TextCompletarCamposUI = null;
    [SerializeField] private GameObject m_TextUsuarioRegistradoUI = null;
    [SerializeField] private GameObject m_BotonEntendidoUI = null;
    public InputField nombreField;
    public InputField apellidoPaternoField;
    public InputField apellidoMaternoField;
    public InputField fechaNacimientoField;
    //public InputField paisField;
    public Dropdown paisDropdown;
    public InputField correoField;
    public InputField contraField;
    public InputField confContraField;
    // Botones
    public Button CrearCuentaGUI;
    public Button LoginGUI;
    public Button EntendidoGUI;
    // Variables para validación
    public bool NOMB = false;
    public bool APAT = false;
    public bool AMAT = false;
    public bool FNAC = false;
    public bool PAIS = false;
    public bool CORR = false;
    public bool CONT = false;
    public bool CONF = false;

    // Variables privadas
    private string NombreVar;
    private string ApePatVar;
    private string ApeMatVar;
    private string FecNacVar;
    private Dropdown paisDropVar;
    private string PaisVar;
    private string CorreoVar;
    private string ContraVar;
    private string ConfContraVar;
    //private bool CorreoValido = false;
    
    // Botones
    private Button btnCrearCuenta;
    private Button btnLogin;
    public Button btnEntendido;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Escuchar clic de botones
        btnCrearCuenta  = CrearCuentaGUI.GetComponent<Button>();
        btnLogin        = LoginGUI.GetComponent<Button>();
        btnEntendido    = EntendidoGUI.GetComponent<Button>();
        btnCrearCuenta.onClick.AddListener(CrearCuenta);
        btnLogin.onClick.AddListener(irIniciarSesion);
        btnEntendido.onClick.AddListener(Entendido);
    }

    // Guardar datos de la GUI
    public void ObtenerDatos()
    {
        NombreVar       = nombreField.GetComponent<InputField>().text;
        ApePatVar       = apellidoPaternoField.GetComponent<InputField>().text;
        ApeMatVar       = apellidoMaternoField.GetComponent<InputField>().text;
        FecNacVar       = fechaNacimientoField.GetComponent<InputField>().text;
        //PaisVar         = paisField.GetComponent<InputField>().text;
        paisDropVar     = paisDropdown.GetComponent<Dropdown>();
        PaisVar         = paisDropVar.options[paisDropVar.value].text;
        CorreoVar       = correoField.GetComponent<InputField>().text;
        ContraVar       = contraField.GetComponent<InputField>().text;
        ConfContraVar   = confContraField.GetComponent<InputField>().text;
    }

    // GUI Login SÍ _ GUI Registrar NO 
    public void irIniciarSesion()
    {
        m_interfazregistrarUI.SetActive(false);
        m_interfazloginUI.SetActive(true);
        LimpiarCampos();
    }

    public void CrearCuenta()
    {
        ReestablecerBooleanos(); 
        // Obtener datos de la GUI
        ObtenerDatos();

        if(NombreVar!="" && ApePatVar!="" && ApeMatVar!="" && FecNacVar!="" && PaisVar!="" && CorreoVar!="" && ContraVar!="" && ConfContraVar!="")
        {
            ValidarDatos();

            // Evaluar si todos los campos son verdaderos
            if(NOMB==true && APAT==true && AMAT==true && FNAC==true && PAIS==true && CORR==true && CONT==true && CONF==true)
            {
                // Llamar a la function
                string url = "https://registrarusuariowasher.azurewebsites.net/api/HttpTriggerFunction01?nombre="+NombreVar+"&apepat="+ApePatVar+"&apemat="+ApeMatVar+"&fecnac="+FecNacVar+"&pais="+PaisVar+"&correo="+CorreoVar+"&contrasena="+ContraVar;
                Debug.Log(""+url);
                // Mensaje de usuario registrado
                m_PanelAvisoUI.SetActive(true);
                m_BackgroundUI.SetActive(true);
                m_TextUsuarioRegistradoUI.SetActive(true);
                m_BotonEntendidoUI.SetActive(true);
                Debug.Log("<color=green>Message:</color> Data register successful...");
                return;
            }
        }
        else
        {
            // Mensaje de falta rellenar campos
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextCompletarCamposUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            Debug.Log("<color=red>Message:</color> Missing fill fields...");
            return;
        }
        LimpiarCampos();
    }

    // Limpiar campos
    public void LimpiarCampos()
    {
        nombreField.GetComponent<InputField>().text="";
        apellidoPaternoField.GetComponent<InputField>().text="";
        apellidoMaternoField.GetComponent<InputField>().text="";
        fechaNacimientoField.GetComponent<InputField>().text="";
        //paisField.GetComponent<InputField>().text="";
        paisDropVar.value = 0;
        correoField.GetComponent<InputField>().text="";
        contraField.GetComponent<InputField>().text="";
        confContraField.GetComponent<InputField>().text="";
    }

    // Validación de datos correctos
    public void ValidarDatos()
    {
        // Si el formato de NOMBRE es inválido
        if (!Regex.Match(NombreVar, "^[A-Z][a-zA-Z]*$").Success)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextNomUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }

        // Si el formato de APELLIDO PATERNO es inválido
        if (!Regex.Match(ApePatVar, "^[A-Z][a-zA-Z]*$").Success)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextApePatUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }

        // Si el formato de APELLIDO MATERNO es inválido
        if (!Regex.Match(ApeMatVar, "^[A-Z][a-zA-Z]*$").Success)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextApeMatUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }

        /*
        // Si el formato de FECHA NACIMIENTO es inválido (MM/DD/YYYY)
        if (!Regex.Match(ApeMatVar, @"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$").Success)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextFecNacUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }
        */

        // Si el formato de CORREO es inválido
        if (!Regex.Match(CorreoVar, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z").Success)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextCorreoUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }

        // Si el formato de CONTRASEÑA es inválido
        // Al menos una letra mayúscula
        // Al menos una letra minúscula
        // Al menos un dígito
        // Al menos un carácter especial
        // Mínimo 8 de longitud
        if (!Regex.Match(ContraVar, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").Success)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextContraUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }

        // Si las CONTRASEÑAS no coinciden
        if (ContraVar!=ConfContraVar)
        {
            // Mensaje de error
            m_PanelAvisoUI.SetActive(true);
            m_BackgroundUI.SetActive(true);
            m_TextContrasNoCoincidenUI.SetActive(true);
            m_BotonEntendidoUI.SetActive(true);
            return;
        }


        // Si todos los datos son válidos
        NOMB = true;
        APAT = true;
        AMAT = true;
        FNAC = true;
        PAIS = true;
        CORR = true;
        CONT = true;
        CONF = true;
    }

    public void ReestablecerBooleanos()
    {
        NOMB = false;
        APAT = false;
        AMAT = false;
        FNAC = false;
        PAIS = false;
        CORR = false;
        CONT = false;
        CONF = false;
    }

    public void Entendido()
    {
        m_PanelAvisoUI.SetActive(false);
        m_BackgroundUI.SetActive(false);
        m_TextNomUI.SetActive(false);
        m_TextApePatUI.SetActive(false);
        m_TextApeMatUI.SetActive(false);
        m_TextFecNacUI.SetActive(false);
        m_TextPaisUI.SetActive(false);
        m_TextCorreoUI.SetActive(false);
        m_TextContraUI.SetActive(false);
        m_TextConfContraUI.SetActive(false);
        m_TextContrasNoCoincidenUI.SetActive(false);
        m_TextCompletarCamposUI.SetActive(false);
        m_TextUsuarioRegistradoUI.SetActive(false);
        m_BotonEntendidoUI.SetActive(false);
    }

}