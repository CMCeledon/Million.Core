import React from 'react';
import PropertyList from './PropertyList';
import Header from '../../navegation/Header';
import Footer from '../../navegation/Footer';

const PropertiesPage = () => {
    return (
        <div className="flex flex-col min-h-screen bg-dark-primary">
            {/* Header toma todo el ancho */}
            <Header />
            
            {/* Main sin contenedor para que PropertyList tome todo el ancho */}
            <main className="flex-grow w-full">
                <PropertyList />
            </main>
            
            {/* Footer toma todo el ancho */}
            <Footer />
        </div>
    );
};

export default PropertiesPage;