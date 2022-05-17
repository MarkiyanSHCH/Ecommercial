import {BrowserRouter} from "react-router-dom";
import AppRouter from "components/AppRouter";
import Footer from 'components/core/Footer/Footer';
import Navbar from 'components/core/Header/Navbar';
import 'styles/AppStyle.css';

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <AppRouter />
      <Footer />
    </BrowserRouter>
  );
}

export default App;
