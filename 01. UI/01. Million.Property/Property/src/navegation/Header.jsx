import React, { useState } from 'react';

const Header = () => {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    
    const toggleMenu = () => setIsMenuOpen(!isMenuOpen);
    
    return (
        <header className="bg-gradient-to-r from-brand-red via-red-800 to-brand-red text-white shadow-lg sticky top-0 z-50 w-full">
            <div className="bg-gray-900 bg-opacity-40 backdrop-blur-sm">
                <div className="max-w-7xl mx-auto px-4 py-2">
                    <div className="flex justify-between items-center text-xs md:text-sm">
                        <div className="flex items-center gap-4">
                            <a  className="flex items-center gap-1 text-gray-200 hover:text-gray-200">
                                <span>📞</span>
                                <span className="hidden sm:inline">+57 3004559711</span>
                            </a>
                            <a className="flex items-center gap-1 text-gray-200 hover:text-gray-200">
                                <span>✉️</span>
                                <span className="hidden md:inline">carlos@cmceledon.com</span>
                            </a>
                        </div>
                        <div className="text-gray-300">
                            <span className="hidden sm:inline">TECHNICAL TEST SR DEVELOPER FULLSTACK - MILLION</span>
                        </div>
                    </div>
                </div>
            </div>

            {/* Navegación principal */}
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-20">
                    {/* Logo y título */}
                    <div className="flex items-center gap-4">
                        <div className="hidden md:block">
                            <h1 className="text-2xl lg:text-3xl font-bold tracking-tight">
                                <span className="text-white">CMCELEDON</span>
                            </h1>
                            <p className="text-xs text-gray-200 tracking-wide">Propiedades Premium</p>
                        </div>
                    </div>

                    {/* Navegación desktop */}
                    <nav className="hidden lg:flex items-center gap-8">
                        <button className="bg-white text-brand-red px-6 py-2.5 rounded-full font-bold hover:bg-gray-100 transition-all duration-300 shadow-lg hover:shadow-xl hover:scale-105 transform">
                            Propiedades
                        </button>
                    </nav>

                    {/* Botón móvil */}
                    <button 
                        onClick={toggleMenu}
                        className="lg:hidden p-2 rounded-lg hover:bg-red-900 transition-colors"
                        aria-label="Toggle menu"
                    >
                        {isMenuOpen ? (
                            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        ) : (
                            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                            </svg>
                        )}
                    </button>
                </div>
            </div>

            {/* Menú móvil */}
            {isMenuOpen && (
                <div className="lg:hidden bg-red-900 border-t border-red-800">
                    <nav className="max-w-7xl mx-auto px-4 py-4 space-y-3">
                        <button className="w-full bg-white text-brand-red px-6 py-3 rounded-lg font-bold hover:bg-gray-100 transition-colors shadow-lg">
                           Propiedades
                        </button>
                    </nav>
                </div>
            )}
        </header>
    );
};

export default Header;