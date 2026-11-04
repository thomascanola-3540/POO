import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    // La API solo autoriza este puerto (CORS en Program.cs), por eso no se permite otro.
    port: 5173,
    strictPort: true,
  },
});
