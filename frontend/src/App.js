import React from "react";
import CalculatorForm from "./CalculatorForm";
import './CalculatorForm.css';

function App() {
  return (
    <div className="page">
      <h1 className="header">
        Generic GW dice calculator
      </h1>
      <CalculatorForm />
    </div>
  )
}

export default App;
