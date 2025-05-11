import './App.css'
import { BrowserRouter as Router, Routes, Route, Outlet } from 'react-router-dom'
import Main from './pages/Main'
import CarPage from './pages/CarPage'
import Login from './pages/Login'
import Register from './pages/Register'
import Header from './components/Header'
import Footer from './components/Footer'
import 'bootstrap/dist/css/bootstrap.min.css'
import Filials from './pages/Filials'
import AdminCars from './pages/admin/AdminCars'
import AdminUsers from './pages/admin/AdminUsers'
import AdminOrders from './pages/admin/AdminOrders'


function App() {

    return (
        <Router>
            <Header />
            <main className="main-content">
            <Routes>
                <Route path="/" element={<Main />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/cars/:id" element={<CarPage />} />
                    <Route path="/filials" element={<Filials />} />
                    <Route path="/admin/cars" element={<AdminCars />} />
                    <Route path="/admin/users" element={<AdminUsers />} />
                    <Route path="/admin/orders" element={<AdminOrders />} />
                </Routes>
            </main>
            <Outlet />
            <Footer />
        </Router>
    )
}

export default App
