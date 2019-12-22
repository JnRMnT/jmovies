import { ToImdbIdPipe } from './to-imdbid.pipe';

describe('ToImdbidPipe', () => {
  it('create an instance', () => {
    const pipe = new ToImdbIdPipe();
    expect(pipe).toBeTruthy();
  });
});
