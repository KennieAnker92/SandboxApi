import './Hero.css';
import heroImage from '../../assets/hero.png';

export function Hero() {
  const handleScroll = () => {
    window.scrollTo({
      top: window.innerHeight * 0.9,
      behavior: 'smooth'
    });
  };

  return (
    <section className="hero-sandbox">
      <div className="hero-sandbox-content">
        <span className="hero-sandbox-tag">SandboxApi</span>
        <h1 className="hero-sandbox-title">
          Engineered for <br />Next-Gen Architecture!!!!!.
        </h1>
        <p className="hero-sandbox-subtitle">
          Seamlessly connect your frontend to secure, high-performance data streams.
        </p>
        <div className="hero-sandbox-actions">
          <button className="hero-sandbox-btn" onClick={handleScroll}>Explore Catalog</button>
        </div>
      </div>

      <div className="hero-sandbox-visual">
        <img 
          src={heroImage} 
          alt="SandboxApi Hardware Robot" 
          className="hero-sandbox-img"
        />
      </div>

      <button 
        className="hero-scroll-btn" 
        onClick={handleScroll} 
        aria-label="Scroll to products"
      >
        <svg 
          width="24" 
          height="24" 
          viewBox="0 0 24 24" 
          fill="none" 
          stroke="currentColor" 
          strokeWidth="2" 
          strokeLinecap="round"
          strokeLinejoin="round"
        >
          <path d="M12 5v14M19 12l-7 7-7-7"/>
        </svg>
      </button>
    </section>
  );
}