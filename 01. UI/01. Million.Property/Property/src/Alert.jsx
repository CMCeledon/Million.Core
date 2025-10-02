import React from 'react';

const Alert = ({ message, onClose }) => {
  if (!message) return null;
  return (
    <div style={{
      position: 'fixed',
      top: 24,
      left: '50%',
      transform: 'translateX(-50%)',
      background: '#f44336',
      color: '#fff',
      padding: '12px 24px',
      borderRadius: 8,
      boxShadow: '0 2px 8px #0002',
      zIndex: 9999,
      minWidth: 200,
      fontWeight: 500,
    }}>
      {message}
      <button onClick={onClose} style={{marginLeft:16, background:'none', color:'#fff', border:'none', fontWeight:'bold', cursor:'pointer'}}>X</button>
    </div>
  );
};

export default Alert;
