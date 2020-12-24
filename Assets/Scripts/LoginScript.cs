using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginScript : MonoBehaviour
{
    // Variables GUI
    [SerializeField] private GameObject m_interfazloginUI = null;
    [SerializeField] private GameObject m_interfazregistrarUI = null;
    public InputField correoField;
    public InputField contraField;
    // Botones
    public Button CrearCuentaGUI;
    public Button LoginGUI;
    public Button OlvideContraGUI;

    // Variables privadas
    private string CorreoVar;
    private string ContraVar;
    // Botones
    private Button btnCrearCuenta;
    private Button btnLogin;
    private Button btnOlvideContra;
    
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
        btnOlvideContra = OlvideContraGUI.GetComponent<Button>();
        btnCrearCuenta.onClick.AddListener(irCrearCuenta);
        btnLogin.onClick.AddListener(Login);
    }

    // Guardar datos de la GUI
    public void ObtenerDatos()
    {
        CorreoVar   = correoField.GetComponent<InputField>().text;
        ContraVar   = contraField.GetComponent<InputField>().text;
    }

    // Evaluar credenciales correctas
    public void Login()
    {
        if(CorreoVar!="" && ContraVar!="")
        {
            LimpiarCampos();
            Debug.Log("<color=green>Message:</color> Entering...");
        }
        else{
            Debug.Log("<color=red>Message:</color> Incorrect credentials...");
        }
    }

    // GUI Login NO _ GUI Registrar SÍ
    public void irCrearCuenta()
    {
        m_interfazloginUI.SetActive(false);
        m_interfazregistrarUI.SetActive(true);
        LimpiarCampos();
    }

    public void LimpiarCampos()
    {   
        correoField.GetComponent<InputField>().text="";
        contraField.GetComponent<InputField>().text="";
    }

}
