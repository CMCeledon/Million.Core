import React, { useEffect, useState } from 'react';
import { fetchPropertyDetail } from './propertyService';

const PropertyDetail = ({ id, onBack }) => {
  const [property, setProperty] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [selectedImage, setSelectedImage] = useState(0);

  useEffect(() => {
    setLoading(true);
    fetchPropertyDetail(id)
      .then(setProperty)
      .catch(setError)
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) {
    return (
      <div className="min-h-screen bg-dark-primary flex items-center justify-center">
        <div className="text-center">
          <div className="inline-block animate-spin rounded-full h-16 w-16 border-b-2 border-brand-red"></div>
          <p className="mt-4 text-gray-600 text-lg">Cargando detalles...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-dark-primary flex items-center justify-center p-4">
        <div className="bg-white rounded-xl shadow-lg p-8 max-w-md w-full text-center">
          <div className="text-6xl mb-4">⚠️</div>
          <h2 className="text-2xl font-bold text-gray-800 mb-2">Error al cargar</h2>
          <p className="text-gray-600 mb-6">{error.message}</p>
          <button
            onClick={onBack}
            className="px-6 bg-brand-red py-3 text-white rounded-lg font-semibold"
          >
            Volver al listado
          </button>
        </div>
      </div>
    );
  }

  if (!property) return null;

  const images = property.images || [];
  const hasImages = images.length > 0 && images[0]?.file;

  return (
    <div className="min-h-screen bg-dark-primary">
      {/* Header con botón de regreso */}
      <div className="bg-white shadow-md sticky top-0 z-40">
        <div className="max-w-7xl mx-auto px-4 py-4">
          <button
            onClick={onBack}
            className="flex bg-brand-red items-center gap-2 font-semibold"
          >
            <span className="text-xl">←</span>
            <span>Volver al listado</span>
          </button>
        </div>
      </div>

      {/* Contenido principal */}
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Columna izquierda: Galería de imágenes */}
          <div className="space-y-4">
            {/* Imagen principal */}
            <div className="bg-white rounded-xl overflow-hidden shadow-lg">
              {hasImages ? (
                <img
                  src={images[selectedImage]?.file}
                  alt={property.name}
                  className="w-full h-96 object-cover"
                />
              ) : (
                <div className="w-full h-96 bg-gradient-to-br from-gray-100 to-gray-200 flex items-center justify-center">
                  <span className="text-9xl">🏠</span>
                </div>
              )}
            </div>

            {/* Miniaturas */}
            {hasImages && images.length > 1 && (
              <div className="grid grid-cols-4 gap-3">
                {images.map((img, idx) => (
                  <button
                    key={idx}
                    onClick={() => setSelectedImage(idx)}
                    className={`rounded-lg overflow-hidden border-2 transition-all ${
                      selectedImage === idx
                        ? 'border-brand-red scale-105'
                        : 'border-transparent hover:border-gray-300'
                    }`}
                  >
                    <img
                      src={img.file}
                      alt={`Vista ${idx + 1}`}
                      className="w-full h-20 object-cover"
                    />
                  </button>
                ))}
              </div>
            )}
          </div>

          {/* Columna derecha: Información de la propiedad */}
          <div className="space-y-6">
            {/* Card principal */}
            <div className="bg-white rounded-xl shadow-lg p-6">
              <div className="flex items-start justify-between mb-4">
                <div>
                  <h1 className="text-3xl font-bold text-gray-800 mb-2">{property.name}</h1>
                  {property.codeInternal && (
                    <p className="text-sm text-gray-500">Código: {property.codeInternal}</p>
                  )}
                </div>
                {property.year && (
                  <span className="bg-brand-red text-white px-4 py-2 rounded-full font-semibold">
                    {property.year}
                  </span>
                )}
              </div>

              <div className="flex items-center gap-2 text-gray-600 mb-6 pb-6 border-b">
                <span className="text-xl">📍</span>
                <p className="text-lg">{property.address}</p>
              </div>

              {/* Precio destacado */}
              <div className="bg-gradient-to-r from-brand-red to-red-900 text-white rounded-lg p-6 mb-6">
                <p className="text-sm opacity-90 mb-1">Precio de venta</p>
                <p className="text-4xl font-bold">
                  ${property.price?.toLocaleString('es-CO')}
                </p>
              </div>

              {/* Características rápidas */}
              <div className="grid grid-cols-2 gap-4">
                {property.year && (
                  <div className="bg-gray-50 rounded-lg p-4">
                    <p className="text-sm text-gray-500 mb-1">Año de construcción</p>
                    <p className="text-xl font-semibold text-gray-800">{property.year}</p>
                  </div>
                )}
                {images.length > 0 && (
                  <div className="bg-gray-50 rounded-lg p-4">
                    <p className="text-sm text-gray-500 mb-1">Imágenes</p>
                    <p className="text-xl font-semibold text-gray-800">{images.length} fotos</p>
                  </div>
                )}
              </div>
            </div>

            {/* Información del propietario */}
            {property.owner && (
              <div className="bg-white rounded-xl shadow-lg p-6">
                <h2 className="text-xl font-bold text-gray-800 mb-4 flex items-center gap-2">
                  <span>👤</span>
                  Información del Propietario
                </h2>
                <div className="flex items-start gap-4">
                  {property.owner.photo && (
                    <img
                      src={property.owner.photo}
                      alt={property.owner.name}
                      className="w-20 h-20 rounded-full object-cover border-4 border-gray-100"
                    />
                  )}
                  <div className="flex-1">
                    <p className="font-semibold text-lg text-gray-800 mb-2">{property.owner.name}</p>
                    <div className="space-y-1 text-sm text-gray-600">
                      <p className="flex items-center gap-2">
                        <span>📍</span>
                        {property.owner.address}
                      </p>
                      <p className="flex items-center gap-2">
                        <span>🎂</span>
                        {new Date(property.owner.birthday).toLocaleDateString('es-CO')}
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            )}
          </div>
        </div>

        {/* Historial de la propiedad */}
        {property.traces && property.traces.length > 0 && (
          <div className="mt-8">
            <div className="bg-white rounded-xl shadow-lg p-6">
              <h2 className="text-2xl font-bold text-gray-800 mb-6 flex items-center gap-2">
                <span>📋</span>
                Historial de Transacciones
              </h2>
              <div className="space-y-4">
                {property.traces.map((trace, idx) => (
                  <div
                    key={idx}
                    className="border border-gray-200 rounded-lg p-5 hover:shadow-md transition-shadow"
                  >
                    <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
                      <div className="flex-1">
                        <h3 className="font-semibold text-lg text-gray-800 mb-2">{trace.name}</h3>
                        <p className="text-sm text-gray-500">
                          {new Date(trace.dateSale).toLocaleDateString('es-CO', {
                            year: 'numeric',
                            month: 'long',
                            day: 'numeric'
                          })}
                        </p>
                      </div>
                      <div className="text-right">
                        <p className="text-sm text-gray-500 mb-1">Valor</p>
                        <p className="text-2xl font-bold text-brand-red">
                          ${trace.value?.toLocaleString('es-CO')}
                        </p>
                        {trace.tax > 0 && (
                          <p className="text-sm text-gray-600 mt-1">
                            Impuesto: ${trace.tax?.toLocaleString('es-CO')}
                          </p>
                        )}
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default PropertyDetail;