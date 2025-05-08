import './App.css'
import { BrowserRouter as Router, Routes, Route, Outlet } from 'react-router-dom'
import Main from './pages/Main'
import About from './pages/About'
import Header from './components/Header'
import Footer from './components/Footer'

function App() {

    return (
        <Router>
            <Header />
            <main className="main-content">
            <Routes>
                <Route path="/" element={<Main />} />
                <Route path="/about" element={<About />} />
                </Routes>
            </main>
            <Outlet />
            <Footer />
        </Router>
    )
}

export default App
