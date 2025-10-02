import React, { useState } from 'react';

const PropertyFilters = ({ onChange }) => {
  const [name, setName] = useState('');
  const [address, setAddress] = useState('');
  const [minPrice, setMinPrice] = useState('');
  const [maxPrice, setMaxPrice] = useState('');
  const [isExpanded, setIsExpanded] = useState(false);

  const formatCurrency = (value) => {
    if (!value) return '';
    const numValue = value.replace(/\D/g, '');
    return new Intl.NumberFormat('es-CO').format(numValue);
  };

  const handleMinPriceChange = (e) => {
    const value = e.target.value.replace(/\D/g, '');
    setMinPrice(value);
  };

  const handleMaxPriceChange = (e) => {
    const value = e.target.value.replace(/\D/g, '');
    setMaxPrice(value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const filters = {};
    
    if (name.trim()) filters.name = name.trim();
    if (address.trim()) filters.address = address.trim();
    if (minPrice) filters.minPrice = parseFloat(minPrice);
    if (maxPrice) filters.maxPrice = parseFloat(maxPrice);
    
    onChange(filters);
  };

  const handleClear = () => {
    setName('');
    setAddress('');
    setMinPrice('');
    setMaxPrice('');
    onChange({});
  };

  const hasFilters = name || address || minPrice || maxPrice;

  // Rangos de precios predefinidos
  const priceRanges = [
    { label: 'Hasta $100M', min: 0, max: 100000000 },
    { label: '$100M - $300M', min: 100000000, max: 300000000 },
    { label: '$300M - $500M', min: 300000000, max: 500000000 },
    { label: '$500M - $1.000M', min: 500000000, max: 1000000000 },
    { label: 'Más de $1.000M', min: 1000000000, max: null },
  ];

  const setPriceRange = (min, max) => {
    setMinPrice(min ? String(min) : '');
    setMaxPrice(max ? String(max) : '');
  };

  return (
    <div className="bg-white rounded-xl shadow-lg p-6 mb-8">
      {/* Header del filtro */}
      <div className="flex items-center justify-between mb-6">
        <div className="flex items-center gap-3">
          <span className="text-2xl">🔍</span>
          <h2 className="text-xl font-bold text-gray-800">Filtros de Búsqueda</h2>
          {hasFilters && (
            <span className="bg-brand-red text-white text-xs px-2 py-1 rounded-full">
              Activos
            </span>
          )}
        </div>
        <button
          onClick={() => setIsExpanded(!isExpanded)}
          className="lg:hidden text-brand-red font-semibold"
        >
          {isExpanded ? 'Ocultar' : 'Mostrar'}
        </button>
      </div>

      <form onSubmit={handleSubmit} className={`space-y-6 ${isExpanded ? 'block' : 'hidden lg:block'}`}>
        {/* Filtros de texto */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              🏠 Nombre de la propiedad
            </label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Ej: Apartamento moderno..."
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-brand-red focus:border-transparent transition-all"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              📍 Dirección o zona
            </label>
            <input
              type="text"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
              placeholder="Ej: Chapinero, Bogotá..."
              className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-brand-red focus:border-transparent transition-all"
            />
          </div>
        </div>

        {/* Rangos de precio predefinidos */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-3">
            💰 Rangos de precio rápidos
          </label>
          <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-2">
            {priceRanges.map((range, idx) => {
              const isActive = 
                (minPrice === String(range.min) || (minPrice === '' && range.min === 0)) &&
                (maxPrice === (range.max ? String(range.max) : '') || (maxPrice === '' && !range.max));
              
              return (
                <button
                  key={idx}
                  type="button"
                  onClick={() => setPriceRange(range.min, range.max)}
                  className={`px-3 py-2 rounded-lg text-sm font-medium transition-all ${
                    isActive
                      ? 'bg-brand-red text-white shadow-md'
                      : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
                  }`}
                >
                  {range.label}
                </button>
              );
            })}
          </div>
        </div>

        {/* Rango de precio personalizado */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-3">
            💵 Rango de precio personalizado
          </label>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-xs text-gray-500 mb-1">Precio mínimo</label>
              <div className="relative">
                <span className="absolute left-4 top-3 text-gray-500">$</span>
                <input
                  type="text"
                  value={formatCurrency(minPrice)}
                  onChange={handleMinPriceChange}
                  placeholder="0"
                  className="w-full pl-8 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-brand-red focus:border-transparent transition-all"
                />
              </div>
            </div>

            <div>
              <label className="block text-xs text-gray-500 mb-1">Precio máximo</label>
              <div className="relative">
                <span className="absolute left-4 top-3 text-gray-500">$</span>
                <input
                  type="text"
                  value={formatCurrency(maxPrice)}
                  onChange={handleMaxPriceChange}
                  placeholder="Sin límite"
                  className="w-full pl-8 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-brand-red focus:border-transparent transition-all"
                />
              </div>
            </div>
          </div>

          {/* Indicador visual del rango */}
          {(minPrice || maxPrice) && (
            <div className="mt-3 p-3 bg-gray-50 rounded-lg">
              <p className="text-sm text-gray-600">
                <span className="font-semibold">Rango seleccionado:</span>{' '}
                {minPrice ? `$${formatCurrency(minPrice)}` : 'Sin mínimo'} - {maxPrice ? `$${formatCurrency(maxPrice)}` : 'Sin máximo'}
              </p>
            </div>
          )}
        </div>

        {/* Botones de acción */}
        <div className="flex flex-col sm:flex-row gap-3 pt-4 border-t">
          <button
            type="submit"
            className="flex-1 bg-brand-red text-white px-6 py-3 rounded-lg font-semibold hover:bg-red-900 transition-colors shadow-md hover:shadow-lg"
          >
            🔍 Buscar Propiedades
          </button>

          {hasFilters && (
            <button
              type="button"
              onClick={handleClear}
              className="flex-1 sm:flex-initial bg-white text-brand-red px-6 py-3 rounded-lg font-semibold border-2 border-brand-red hover:bg-gray-50 transition-colors"
            >
              ✕ Limpiar Filtros
            </button>
          )}
        </div>
      </form>
    </div>
  );
};

export default PropertyFilters;