using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeNode : MonoBehaviour
    {
        private static readonly int TexSize = Shader.PropertyToID("TexSize");
        private static readonly int P1Weight = Shader.PropertyToID("P1Weight");

        private static readonly int[] ChildPos =
        {
            Shader.PropertyToID("P2Pos"),
            Shader.PropertyToID("P3Pos"),
            Shader.PropertyToID("P4Pos"),
            Shader.PropertyToID("P5Pos"),
        };

        private static readonly int[] ChildWeight =
        {
            Shader.PropertyToID("P2Weight"),
            Shader.PropertyToID("P3Weight"),
            Shader.PropertyToID("P4Weight"),
            Shader.PropertyToID("P5Weight"),
        };

        private const int MaxChildren = 4;
        
        private SpriteRenderer _renderer;
        private TreeNode[] _children = new TreeNode[MaxChildren];

        [field: SerializeField] public float Weight { get; set; }
        [SerializeField] private bool inputEnabled = false;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private TreeNode prefab;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetChildrenArray();
        }

        private void CopyChildren(IReadOnlyList<TreeNode> newChildren)
        {
            // ignore first child because it's itself
            for (int i = 0; i < _children.Length; i++)
            {
                int adjustedChildIndex = i + 1;
                if (adjustedChildIndex < newChildren.Count)
                {
                    _children[i] = newChildren[adjustedChildIndex];
                }
                else
                {
                    _children[i] = null;
                }
            }
        }

        private static void CopyArrayOrNull<T>(IReadOnlyList<T> source, IList<T> target)
        {
            for (int i = 0; i < target.Count; i++)
            {
                if (i < source.Count)
                {
                    target[i] = source[i];
                }
                else
                {
                    target[i] = default;
                }
            }
        }

        private void Update()
        {
            ProcessInput();
            AdjustTextureBounds();
            SetMaterialProperties();
        }

        private void ProcessInput()
        {
            if (!inputEnabled) return;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Time.deltaTime * moveSpeed * Vector3.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Time.deltaTime * moveSpeed * Vector3.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Time.deltaTime * moveSpeed * Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Time.deltaTime * moveSpeed * Vector3.right;
            }

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                var numBranches = Random.Range(1, 4);
                SpawnBranch(Random.Range(0.2f, 2f), Random.Range(-15, 15));
                if (numBranches > 1)
                {
                    SpawnBranch(Random.Range(0.3f, 2f), Random.Range(-30, 30));
                }
                if (numBranches > 2)
                {
                    SpawnBranch(Random.Range(0.4f, 2f), Random.Range(-60, 60));
                }
                if (numBranches > 3)
                {
                    SpawnBranch(Random.Range(0.5f, 2f), Random.Range(-90, 90));
                }
                SetChildrenArray();
            }
        }

        public void wideSpawnStep()
        {
            //choose a number of branches to spawn on each leaf node
            var numBranches = Random.Range(1, 4);
            //perform each branch spawn, getting farther and wider for each new branch spawned
            for (int i = 0; i <= numBranches; i++)
            {
                SpawnBranch(Random.Range(0.2f + (i *0.1f), 2f), Random.Range(-15* (i+1), 15 * (i+1)));
            }
            SetChildrenArray();
        }

        public void volumeSpawnStep()
        {

        }

        private void SpawnBranch(float distance, float degreesFromOppositeParent = 0)
        {
            int childCount = _children.Count(c => c != null);
            if (childCount >= MaxChildren) return;

            var newNode = Instantiate(prefab, transform);
            newNode.prefab = prefab;
            newNode.Weight = Weight * 0.67f;
            newNode.inputEnabled = true;
            inputEnabled = false;
            if (transform.parent != null)
            {
                Vector3 toParent = transform.parent.position - transform.position;
                var finalDisplacement = distance * (Quaternion.AngleAxis(degreesFromOppositeParent, Vector3.forward) * toParent.normalized);
                newNode.transform.position = transform.position - finalDisplacement;
            }
            else
            {
                newNode.transform.position = transform.position + Vector3.up;
            }
        }

        private void AdjustTextureBounds()
        {
            foreach (var child in _children)
            {
                if (child == null) continue;
                
                child.transform.parent = null;
            }
            
            var desiredBounds = GetDesiredBounds();
            Vector2 currentSize = _renderer.bounds.extents;
            Vector2 goalScaleRatio = desiredBounds/currentSize;
            transform.localScale = transform.localScale.Multiply(goalScaleRatio.ToVector3(1));
            
            foreach (var child in _children)
            {
                if (child == null) continue;

                child.transform.SetParent(transform, true);
            }
        }

        private Vector2 GetDesiredBounds()
        {
            float biggestXDist = Weight;
            float biggestYDist = Weight;

            if (_children.All(c => c == null))
            {
                return new Vector2(biggestXDist, biggestYDist);
            }

            foreach (TreeNode child in _children)
            {
                if (child == null) continue;
                
                float xDist = Mathf.Abs(transform.position.x - child.transform.position.x) + child.Weight;
                float yDist = Mathf.Abs(transform.position.y - child.transform.position.y) + child.Weight;
                
                if (xDist > biggestXDist)
                {
                    biggestXDist = xDist;
                }
                if (yDist > biggestYDist)
                {
                    biggestYDist = yDist;
                }
            }

            return new Vector2(biggestXDist, biggestYDist);
        }

        private void SetChildrenArray()
        {
            var foundChildren = new List<TreeNode>();
            foreach (Transform childTransform in transform)
            {
                var childParentGameObj = childTransform.parent.gameObject;
                if (childParentGameObj != gameObject) continue;
                
                var component = childTransform.gameObject.GetComponent<TreeNode>();
                if (component != null)
                {
                    foundChildren.Add(component);
                }
            }
            CopyArrayOrNull(foundChildren.ToArray(), _children);
        }

        private void SetMaterialProperties()
        {
            Vector2 texSize = new Vector2(_renderer.bounds.size.x, _renderer.bounds.size.y);
            _renderer.material.SetVector(TexSize, texSize);
            _renderer.material.SetFloat(P1Weight, Weight);

            for (var index = 0; index < _children.Length; index++)
            {
                Vector2 relativePosition = _children[index]?.transform.position - transform.position ?? Vector2.zero;
                float weight = _children[index]?.Weight ?? 0f;
                _renderer.material.SetVector(ChildPos[index], relativePosition);
                _renderer.material.SetFloat(ChildWeight[index], weight);
            }
        }
    }
}
