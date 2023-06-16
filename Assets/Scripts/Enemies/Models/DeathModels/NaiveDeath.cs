namespace Enemies.Models.DieModels {
    public class NaiveDeath : DeathModel {
        public override void Die() {
            Destroy(gameObject);
        }
    }
}