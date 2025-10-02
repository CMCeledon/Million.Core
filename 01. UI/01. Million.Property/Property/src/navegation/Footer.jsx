// Archivo: Footer.jsx

import React from 'react';

const styles = {
    footer: {
        background: '#112240', // Color oscuro similar al header
        color: '#8892b0', // Gris claro para el texto
        padding: '20px 0',
        textAlign: 'center',
        marginTop: 'auto', // Asegura que el footer se pegue al final de la página
        boxShadow: '0 -2px 10px rgba(0, 0, 0, 0.1)',
        width: '100%',
    },
    text: {
        fontSize: '0.9rem',
        margin: '5px 0',
    },
    link: {
        color: '#64ffda', // Color de acento
        textDecoration: 'none',
    }
};

const Footer = () => {
    return (
        <footer style={styles.footer}>
            <p style={styles.text}>
                Desarrollador: <a href="https://www.cmceledon.com" target="_blank" rel="noopener noreferrer" style={styles.link}>Carlos Mario Celedón Rodelo</a> - 2025
            </p>
            <p style={styles.text}>
                Plataforma Inmobiliaria impulsada por MongoDB Atlas y .NET 9
            </p>
        </footer>
    );
};

export default Footer;