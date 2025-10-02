import React, { useState, useEffect } from 'react';
import { fetchPagedProperties } from './propertyService';
import PropertyFilters from './PropertyFilters';
import PropertyDetail from './PropertyDetail';
import Alert from '../../Alert';

const PropertyList = () => {
  const [properties, setProperties] = useState([]);
  const [filters, setFilters] = useState({});
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [showAlert, setShowAlert] = useState(false);
  const [selectedId, setSelectedId] = useState(null);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(12);
  const [total, setTotal] = useState(0);

  useEffect(() => {
    setLoading(true);
    const fullFilters = {
      ...filters,
      pageNumber: page,
      pageSize: pageSize
    };

    fetchPagedProperties(fullFilters)
      .then((pagedRes) => {
        if (pagedRes && pagedRes.data) {
          setProperties(pagedRes.data);
          setTotal(pagedRes.totalItems);
        } else {
          setProperties([]);
          setTotal(0);
        }
      })
      .catch((err) => {
        setError(err);
        setShowAlert(true);
      })
      .finally(() => setLoading(false));
  }, [filters, page, pageSize]);

  if (selectedId) {
    return <PropertyDetail id={selectedId} onBack={() => setSelectedId(null)} />;
  }

  const totalPages = total ? Math.ceil(total / pageSize) : 0;

  return (
    <div className="w-full min-h-screen bg-dark-primary">
      {/* Contenedor principal con ancho completo */}
      <div className="w-full">
        {/* Hero Section */}
        <div className="bg-gradient-to-r from-brand-red to-red-900 text-white py-12 px-4">
          <div className="max-w-7xl mx-auto">
            <h1 className="text-4xl md:text-5xl font-bold mb-4">Encuentra tu Propiedad Ideal</h1>
            <p className="text-lg text-gray-100">Explora nuestra selección exclusiva de propiedades premium</p>
            <div className="mt-6 flex items-center gap-4 text-sm">
              <div className="flex items-center gap-2">
                <span className="text-2xl">🏠</span>
                <span>{total} Propiedades disponibles</span>
              </div>
            </div>
          </div>
        </div>

        {/* Filtros y Contenido */}
        <div className="max-w-7xl mx-auto px-4 py-8">
          <PropertyFilters onChange={setFilters} />

          {/* Loading State */}
          {loading && (
            <div className="flex justify-center items-center py-20">
              <div className="text-center">
                <div className="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-brand-red"></div>
                <p className="mt-4 text-gray-600">Cargando propiedades...</p>
              </div>
            </div>
          )}

          {/* Error Alert */}
          {showAlert && error && (
            <Alert message={error.message} onClose={() => setShowAlert(false)} />
          )}

          {/* No Results */}
          {properties.length === 0 && !loading && (
            <div className="text-center py-20">
              <div className="text-6xl mb-4">🔍</div>
              <h3 className="text-2xl font-bold text-gray-700 mb-2">No se encontraron propiedades</h3>
              <p className="text-gray-500">Intenta ajustar los filtros de búsqueda</p>
            </div>
          )}

          {/* Property Grid */}
          {properties.length > 0 && !loading && (
            <>
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6 mb-8">
                {properties.map((prop) => (
                  <div
                    key={String(prop.id)}
                    onClick={() => setSelectedId(prop.id)}
                    className="bg-white rounded-xl overflow-hidden shadow-md hover:shadow-2xl transition-all duration-300 cursor-pointer transform hover:-translate-y-2 group"
                  >
                    {/* Imagen */}
                    <div className="relative h-48 bg-gray-200 overflow-hidden">
                      {prop.images && prop.images[0] && prop.images[0].file ? (
                        <img
                          src={prop.images[0].file}
                          alt={prop.name}
                          className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500"
                        />
                      ) : (
                        <div className="w-full h-full flex items-center justify-center bg-gradient-to-br from-gray-100 to-gray-200">
                          <span className="text-6xl">🏠</span>
                        </div>
                      )}
                      {/* Badge de Año */}
                      {prop.year && (
                        <div className="absolute top-3 right-3 bg-white bg-opacity-90 px-3 py-1 rounded-full text-xs font-semibold text-gray-700">
                          {prop.year}
                        </div>
                      )}
                    </div>

                    {/* Contenido */}
                    <div className="p-5">
                      <h3 className="text-lg font-bold text-gray-800 mb-2 line-clamp-1 group-hover:text-brand-red transition-colors">
                        {prop.name}
                      </h3>
                      
                      <div className="flex items-start gap-2 text-sm text-gray-600 mb-3">
                        <span className="text-base">📍</span>
                        <p className="line-clamp-2">{prop.address}</p>
                      </div>

                      {prop.codeInternal && (
                        <div className="text-xs text-gray-500 mb-3">
                          Código: {prop.codeInternal}
                        </div>
                      )}

                      <div className="pt-3 border-t border-gray-100">
                        <div className="flex items-center justify-between">
                          <span className="text-xs text-gray-500 uppercase tracking-wide">Precio</span>
                          <span className="text-xl font-bold text-brand-red">
                            ${prop.price?.toLocaleString('es-CO')}
                          </span>
                        </div>
                      </div>

                      {/* Badges adicionales */}
                      <div className="flex gap-2 mt-3">
                        {prop.images && prop.images.length > 0 && (
                          <span className="text-xs bg-gray-100 text-gray-600 px-2 py-1 rounded">
                            📷 {prop.images.length} fotos
                          </span>
                        )}
                        {prop.traces && prop.traces.length > 0 && (
                          <span className="text-xs bg-blue-50 text-blue-600 px-2 py-1 rounded">
                            📋 Historial
                          </span>
                        )}
                      </div>
                    </div>
                  </div>
                ))}
              </div>

              {/* Paginación */}
              {totalPages > 1 && (
                <div className="flex flex-col sm:flex-row items-center justify-center gap-4 py-8">
                  <button
                    onClick={() => setPage(page - 1)}
                    disabled={page === 1}
                    className="px-6 py-3 bg-white border-2 border-brand-red text-brand-red rounded-lg font-semibold hover:bg-brand-red hover:text-white transition-colors disabled:opacity-50 disabled:cursor-not-allowed disabled:hover:bg-white disabled:hover:text-brand-red"
                  >
                    ← Anterior
                  </button>
                  
                  <div className="flex items-center gap-2">
                    {[...Array(totalPages)].map((_, i) => {
                      const pageNum = i + 1;
                      // Mostrar solo páginas cercanas a la actual
                      if (
                        pageNum === 1 ||
                        pageNum === totalPages ||
                        (pageNum >= page - 1 && pageNum <= page + 1)
                      ) {
                        return (
                          <button
                            key={pageNum}
                            onClick={() => setPage(pageNum)}
                            className={`w-10 h-10 rounded-lg font-semibold transition-colors ${
                              page === pageNum
                                ? 'bg-brand-red text-white'
                                : 'bg-white text-gray-700 hover:bg-gray-100 border border-gray-300'
                            }`}
                          >
                            {pageNum}
                          </button>
                        );
                      } else if (pageNum === page - 2 || pageNum === page + 2) {
                        return <span key={pageNum} className="text-gray-400">...</span>;
                      }
                      return null;
                    })}
                  </div>

                  <button
                    onClick={() => setPage(page + 1)}
                    disabled={page === totalPages}
                    className="px-6 py-3 bg-brand-red text-white rounded-lg font-semibold hover:bg-red-900 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    Siguiente →
                  </button>
                </div>
              )}
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default PropertyList;