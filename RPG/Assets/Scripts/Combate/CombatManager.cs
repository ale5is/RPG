using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CombatStatus
{
    WAITING_FOR_FIGTHER,
    FIGTHER_ACTION,
    CHECK_ACTION_MESSAGES,
    CHECK_FOR_VICTORY,
    NEXT_TURN
}

public class CombatManager : MonoBehaviour
{
    // Equipos de los jugadores y enemigos
    public Fighter[] PlayerTeam;
    public Fighter[] EnemyTeam;

    private Fighter[] fighters;
    private int fighterIndex;
    private bool isCombatActive;
    private CombatStatus combatStatus;
    private Skill currentFigtherSkill;
    private bool victoria = false;

    // Referencias a otros objetos
    public static Controller_Player player;
    public GameObject jugador, save;

    private void Awake()
    {
        // Se inicializan algunas variables y se asignan objetos.
    }

    void Start()
    {
        LogPanel.write("Batalla Iniciada");

        // Configurar los luchadores (jugadores y enemigos)
        this.fighters = new Fighter[]
        {
            this.PlayerTeam[0], this.PlayerTeam[1],
            this.EnemyTeam[0], this.EnemyTeam[1],
        };

        foreach (var fgtr in fighters)
        {
            fgtr.combatManager = this;
        }

        // Iniciar el estado de la batalla
        this.combatStatus = CombatStatus.NEXT_TURN;
        this.fighterIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());

        player = FindAnyObjectByType<Controller_Player>();
        save = GameObject.FindGameObjectWithTag("Save");
    }

    private void Update()
    {
        // Aquí podrías incluir algún código para actualizar el estado del juego
    }

    IEnumerator CombatLoop()
    {
        // Bucle principal de combate
        while (this.isCombatActive)
        {
            switch (this.combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGTHER:
                    yield return null;
                    break;

                case CombatStatus.FIGTHER_ACTION:
                    // Ejecutar la acción del luchador
                    LogPanel.write($"{this.fighters[this.fighterIndex].name} uso {currentFigtherSkill.name}.");
                    yield return null;
                    currentFigtherSkill.Run();
                    yield return new WaitForSeconds(currentFigtherSkill.animationDuration);
                    this.combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;
                    break;

                case CombatStatus.CHECK_ACTION_MESSAGES:
                    string nextMessage = this.currentFigtherSkill.TomarSiguienteMensaje();
                    if (nextMessage != null)
                    {
                        LogPanel.write(nextMessage);
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        this.currentFigtherSkill = null;
                        this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                    }
                    break;

                case CombatStatus.CHECK_FOR_VICTORY:
                    // Revisar si alguno de los equipos ha ganado
                    bool arePlayerAlive = false;
                    foreach (var fighter in this.PlayerTeam)
                    {
                        arePlayerAlive |= fighter.isAlive;
                    }

                    bool areEnemyAlive = false;
                    foreach (var fighter in this.EnemyTeam)
                    {
                        areEnemyAlive |= fighter.isAlive;
                    }

                    bool victory = !areEnemyAlive;
                    bool defeat = !arePlayerAlive;

                    if (victory)
                    {
                        LogPanel.write("Victoria");
                        this.isCombatActive = false;
                        int totalXP = CalculateTotalXP();
                        RewardPlayersWithXP(totalXP);
                        SaveAndLoad();
                    }

                    if (defeat)
                    {
                        LogPanel.write("Derrota");
                        this.isCombatActive = false;
                        SaveAndLoad();
                    }

                    if (this.isCombatActive)
                    {
                        this.combatStatus = CombatStatus.NEXT_TURN;
                    }
                    yield return null;
                    break;

                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);
                    Fighter current = null;
                    do
                    {
                        this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;
                        current = this.fighters[this.fighterIndex];
                    }
                    while (!current.isAlive);

                    LogPanel.write($"{current.name} tiene el turno.");
                    current.InitTurn();
                    this.combatStatus = CombatStatus.WAITING_FOR_FIGTHER;
                    break;
            }
        }
    }

    // Método para calcular la experiencia total de los enemigos derrotados
    private int CalculateTotalXP()
    {
        int xpFromEnemy1 = EnemyTeam[0].GetComponent<EnemyFigther>().CalculateXP(PlayerTeam[0].GetComponent<PlayerFigther>().nivel);
        int xpFromEnemy2 = EnemyTeam[1].GetComponent<EnemyFigther>().CalculateXP(PlayerTeam[0].GetComponent<PlayerFigther>().nivel);
        return xpFromEnemy1 + xpFromEnemy2;
    }

    // Método para recompensar a los jugadores con la XP ganada
    private void RewardPlayersWithXP(int totalXP)
    {
        PlayerTeam[0].GetComponent<PlayerFigther>().subirNivel(totalXP);
        PlayerTeam[1].GetComponent<PlayerFigther>().subirNivel(totalXP);
    }

    // Método para guardar y cargar los datos del juego
    private void SaveAndLoad()
    {
        save.GetComponent<Controlador>().GuardarDatos();
        SceneManager.LoadScene(0);
        save.GetComponent<Controlador>().CargarDatos();
    }

    public Fighter GetOpposingFigther()
    {
        return this.fighterIndex == 0 ? this.fighters[1] : this.fighters[0];
    }

    public Fighter[] GetOpposingTeam()
    {
        return this.fighterIndex == 0 || this.fighterIndex == 1 ? this.EnemyTeam : this.PlayerTeam;
    }

    public void OnFigtherSkill(Skill skill)
    {
        this.currentFigtherSkill = skill;
        this.combatStatus = CombatStatus.FIGTHER_ACTION;
    }
}
