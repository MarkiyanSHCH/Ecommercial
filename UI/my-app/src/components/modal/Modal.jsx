import React from 'react';
import './ModalStyle.css';

const Modal = ({ isActive, setActive, children }) => {
  return (
    <>
      {isActive && (
        <div className="myModal" onClick={setActive}>
          <div className="myModalContent" onClick={(e) => e.stopPropagation()}>
            {children}
          </div>
        </div>
      )}
    </>
  );
};

export default Modal;
