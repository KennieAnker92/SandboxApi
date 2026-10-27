import { BrowserRouter, Routes, Route, useNavigate, useParams } from 'react-router-dom';
import { Home } from './pages/Home/Home';
import { ProductDetails } from './pages/Product/ProductDetails';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Landing Page Route */}
        <Route path="/" element={<HomeRoute />} />
        
        {/* Product Details Route with ID parameter */}
        <Route path="/products/:id" element={<ProductDetailsRoute />} />
      </Routes>
    </BrowserRouter>
  );
}

// Wrapper components to bridge React Router hooks with our page components
function HomeRoute() {
  const navigate = useNavigate();
  return <Home onSelectProduct={(id) => navigate(`/products/${id}`)} />;
}

function ProductDetailsRoute() {
  const { id } = useParams();
  const navigate = useNavigate();
  
  // Parse the string parameter from URL to a number
  const productId = id ? parseInt(id, 10) : 0;

  return <ProductDetails productId={productId} onBack={() => navigate('/')} />;
}