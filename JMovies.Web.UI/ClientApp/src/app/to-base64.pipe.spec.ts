import { ToBase64Pipe } from './to-base64.pipe';

describe('ToBase64Pipe', () => {
  it('create an instance', () => {
    const pipe = new ToBase64Pipe();
    expect(pipe).toBeTruthy();
  });
});
