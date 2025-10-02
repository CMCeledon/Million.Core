  
import './App.css';

import ErrorBoundary from './ErrorBoundary';
import PropertiesPage from './features/properties/PropertiesPage';


function App() {
  return (
    <ErrorBoundary>
      <PropertiesPage />
    </ErrorBoundary>
  );
}

export default App;
