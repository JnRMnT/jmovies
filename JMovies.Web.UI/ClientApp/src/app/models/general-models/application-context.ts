import { AuthenticationInfo } from './authentication-info';

export class ApplicationContext {
    authenticationInfo?: AuthenticationInfo;
}

declare global {
    interface Window { JMContext: ApplicationContext; }
}
