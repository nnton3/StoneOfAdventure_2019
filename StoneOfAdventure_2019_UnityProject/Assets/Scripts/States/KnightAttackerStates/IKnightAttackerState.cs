
public interface IKnightAttackerState
{
    void Attack();
    void Spurt();
    void MoveHorizontal(float direction, float movespeed);
    void Dead();
    void Stun(float time);
}
