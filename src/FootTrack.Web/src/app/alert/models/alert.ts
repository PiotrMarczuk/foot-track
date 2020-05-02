export class Alert {
  id: string;
  type: string;
  message: string;
  keepAfterRouteChange: boolean;

  constructor(init?: Partial<Alert>) {
      Object.assign(this, init);
  }
}
