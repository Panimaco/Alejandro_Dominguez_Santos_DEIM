using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour 
{

    //Zona de Variables Globales
    [SerializeField]
    private GameObject _zombiePrefab,
                       _playerObject;

    [SerializeField]
    private float _spawnTime = 3.0f,
                  _timer,
                  _spawnRadius = 10.0f,
                  _angle = 0.0f;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    public static int ZombieCount = 1;

    private void Update() 
    {

        //Convierte el "_playerTransform" en el "transform" del "Player"
        _playerTransform = _playerObject.transform;

        //Temporizador
        _timer += Time.deltaTime;

        // Si el tiempo de espera ha pasado, genera un nuevo zombie
        if (_timer >= _spawnTime && ZombieCount <= 5)
        {

            _timer = 0.0f;
            SpawnZombie();
            ZombieCount++;

        }
    }

    private void SpawnZombie() 
    {

        //Incrementa el ángulo para que la posición varíe en cada spawn
        _angle += 45.0f;

        //Si el ángulo de aparición supera los 360
        if (_angle >= 360.0f) 
        {

            //Resetea el ángulo
            _angle -= 360.0f;
        
        }
        
        //Convierte los angulos en radianes
        float angleInRadians = _angle * Mathf.Deg2Rad,
              //Posición del circulo en x
              offsetX = Mathf.Cos(angleInRadians) * _spawnRadius,
              //Posición del circulo en z
              offsetZ = Mathf.Sin(angleInRadians) * _spawnRadius;

        //Establece la posición de spawn
        Vector3 spawnPosition = new Vector3(

            _playerTransform.position.x + offsetX,
            _playerTransform.position.y,
            _playerTransform.position.z + offsetZ
            
            );

        //Si el personaje está saltando no le importa al spawner de zombies
        if (_playerTransform.position.y > 0) 
        {

            spawnPosition.y = 0;
        
        }

        // Instancia el prefab del zombie en la posición generada
        Instantiate(_zombiePrefab, spawnPosition, Quaternion.identity);

    }
}
